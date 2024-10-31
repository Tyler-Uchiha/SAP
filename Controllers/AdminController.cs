using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAP.Data;
using SAP.Models;
using System.Dynamic;

namespace SAP.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View();
        }
        
        private void GetAllCaseStats()
        {
            // Total cases
            var totalCases = _db.Cases_Table.Count();

            // Open cases with CaseStatus "In-Progress"
            var openCases = _db.Cases_Table.Count(c => c.CaseStatus == "In-Progress");

            // Closed cases with CaseStatus "Resolved"
            var closedCases = _db.Cases_Table.Count(c => c.CaseStatus == "Resolved");

            // Assign to ViewBag
            ViewBag.Total_All_Cases = totalCases;
            ViewBag.Open_All_Cases = openCases;
            ViewBag.Closed_All_Cases = closedCases;
        }

        public void OffenceStats()
        {
            var offenceStats = _db.Cases_Table
                .GroupBy(c => c.OffenceCommitted)
                .Select(g => new
                {
                    OffenceCommitted = g.Key,
                    CrimeCount = g.Count()
                })
                .ToList();

            ViewBag.OffenceStats = offenceStats;
        }

        public IActionResult Dashboard()
        {
            if (User.IsInRole("Superman") ^ User.IsInRole("Admin"))
            {
                GetAllCaseStats();

                var offenceStats = _db.Cases_Table
                    .GroupBy(c => c.OffenceCommitted)
                    .Select(g => new OffenceStatViewModel
                    {
                        OffenceCommitted = g.Key,
                        CrimeCount = g.Count()
                    })
                    .ToList();

                var caseManagerStats = _db.Users
                .Where(u => u.User_Role == "Case Manager")
                .Select(u => new CaseManagerStats
                {
                    FirstName = u.First_Name,
                    LastName = u.Last_Name,
                    TotalCases = _db.Cases_Table.Count(c => c.ManagerId == u.Id),
                    OpenCases = _db.Cases_Table.Count(c => c.ManagerId == u.Id && c.CaseStatus == "In-Progress"),
                    ClosedCases = _db.Cases_Table.Count(c => c.ManagerId == u.Id && c.CaseStatus == "Resolved")
                })
                .ToList();

                dynamic FlexModel = new ExpandoObject();
                FlexModel.Offence_Stats = offenceStats;
                FlexModel.Case_ManagerS_Stats = caseManagerStats;

                return View(FlexModel);
            }
            else
            {
                return RedirectToAction("NoEntry", "Home");
            }
        }

        public IActionResult GetCaseManagerStats()
        {
            if (User.IsInRole("Superman") ^ User.IsInRole("Admin"))
            {
                var caseManagerStats = _db.Users
                .Where(u => u.User_Role == "Case Manager")
                .Select(u => new CaseManagerStats
                {
                    FirstName = u.First_Name,
                    LastName = u.Last_Name,
                    TotalCases = _db.Cases_Table.Count(c => c.ManagerId == u.Id),
                    OpenCases = _db.Cases_Table.Count(c => c.ManagerId == u.Id && c.CaseStatus == "In-Progress"),
                    ClosedCases = _db.Cases_Table.Count(c => c.ManagerId == u.Id && c.CaseStatus == "Resolved")
                })
                .ToList();

                return View(caseManagerStats);
            }
            else
            {
                return RedirectToAction("NoEntry", "Home");
            } 
        }
    }
}
