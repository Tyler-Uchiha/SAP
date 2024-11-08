﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SAP.Data;
using SAP.Migrations;
using SAP.Models;
using System.ComponentModel;
using System.Dynamic;
using System.Threading.Tasks;

namespace SAP.Controllers
{
    public class StationManagerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StationManagerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get all case managers
        //public IActionResult GetCaseManagers()
        //{
        //    //To access the station manager pages
        //    if (User.IsInRole("Superman") ^ User.IsInRole("Station Manager"))
        //    {
        //        var CaseManagers = _db.Users
        //        .Where(u => u.User_Role == "Case Manager")
        //        .Select(u => new CaseManagerCasesSM
        //        {
        //            ManagerID = u.Id,
        //            First_Name = u.First_Name,
        //            Last_Name = u.Last_Name,
        //            Total_Cases = _db.Cases_Table.Count(c => c.ManagerId == u.Id),
        //            Open_Cases = _db.Cases_Table.Count(c => c.ManagerId == u.Id && c.CaseStatus == "In-Progress"),
        //            Closed_Cases = _db.Cases_Table.Count(c => c.ManagerId == u.Id && c.CaseStatus == "Resolved")
        //        })
        //        .ToList();

        //        ViewBag.CaseManagers = CaseManagers;

        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoEntry", "Home");
        //    }
        //}

        //The terminate role method

        public IActionResult TerminateManager(string ManagerID)
        {
            if (User.IsInRole("Superman") ^ User.IsInRole("Station Manager"))
            {
                var manager = _db.Users.FirstOrDefault(u => u.Id == ManagerID);
                if (manager != null)
                {
                    manager.User_Role = "Terminated";
                    _db.SaveChanges();
                }
                return RedirectToAction("GetCaseManagers");
            }
            else
            {
                return RedirectToAction("NoEntry", "Home");
            }
        }

        public IActionResult RestoreManager(string ManagerID)
        {
            if (User.IsInRole("Superman") ^ User.IsInRole("Station Manager"))
            {
                var manager = _db.Users.FirstOrDefault(u => u.Id == ManagerID);
                if (manager != null)
                {
                    manager.User_Role = "Case Manager";
                    _db.SaveChanges();
                }
                return RedirectToAction("GetCaseManagers");
            }
            else
            {
                return RedirectToAction("NoEntry", "Home");
            }
        }

        public IActionResult GetCaseManagers()
        {
            if (User.IsInRole("Superman") ^ User.IsInRole("Station Manager"))
            {
                var CaseManagers = _db.Users
                .Where(u => u.User_Role == "Case Manager")
                .Select(u => new CaseManagerCasesSM
                {
                    ManagerID = u.Id,
                    First_Name = u.First_Name,
                    Last_Name = u.Last_Name,
                    Total_Cases = _db.Cases_Table.Count(c => c.ManagerId == u.Id),
                    Open_Cases = _db.Cases_Table.Count(c => c.ManagerId == u.Id && c.CaseStatus == "In-Progress"),
                    Closed_Cases = _db.Cases_Table.Count(c => c.ManagerId == u.Id && c.CaseStatus == "Resolved")
                })
                .ToList();
                ViewBag.CaseManagers = CaseManagers;
                return View();
            }
            else
            {
                return RedirectToAction("NoEntry", "Home");
            }
        }



        //Cases per manager
        public IActionResult CasesPerManager(string ManagerID)
        {
            if (User.IsInRole("Superman") ^ User.IsInRole("Station Manager"))
            {
                var Assigned_Cases = _db.Cases_Table
                                .Where(u => u.ManagerId == ManagerID)
                                .Select(u => new CasesPerManagerRecs
                                {
                                    ManagerIdenti = u.ManagerId,
                                    CriminalRecId = u.CriminalRecordId,
                                    CriminalIdNum = u.CriminalIdNum,
                                    Sentence = u.Sentence,
                                    AssignedTo = u.AssignedTo,
                                    CaseStatus = u.CaseStatus

                                }).ToList();

                ViewBag.Assigned_Cases = Assigned_Cases;
                return View();
            }
            else
            {
                return RedirectToAction("NoEntry", "Home");
            } 
        }

        //// Case Manager Action
        //public IActionResult ManageCaseManagerCases()
        //{
        //    var caseManagerCases = _db.Users
        //        .Where(u => u.User_Role == "Case Manager")
        //        .SelectMany(u => _db.Cases_Table.Where(c => c.ManagerId == u.Id),
        //                    (u, c) => new CaseManagerCasesViewModel
        //                    {
        //                        FirstName = u.First_Name,
        //                        LastName = u.Last_Name,
        //                        CriminalRecordId = c.CriminalRecordId,
        //                        CaseStatus = c.CaseStatus,
        //                        UserId = u.Id
        //                    })
        //        .ToList();

        //    return View(caseManagerCases);
        //}

        //// Police Officer Action
        //public IActionResult ManagePoliceOfficerCases()
        //{
        //    var policeOfficerCases = _db.Users
        //        .Where(u => u.User_Role == "Police Officer")
        //        .SelectMany(u => _db.Cases_Table.Where(c => c.ManagerId == u.Id),
        //                    (u, c) => new PoliceOfficerCasesViewModel
        //                    {
        //                        FirstName = u.First_Name,
        //                        LastName = u.Last_Name,
        //                        CriminalRecordId = c.CriminalRecordId,
        //                        CaseStatus = c.CaseStatus,
        //                        UserId = u.Id
        //                    })
        //        .ToList();

        //    return View(policeOfficerCases);
        //}

        //// Terminate Role Action
        //[HttpPost]
        //public IActionResult TerminateRole(string userId, string role)
        //{
        //    var user = _db.Users.Find(userId);
        //    if (user != null)
        //    {
        //        user.User_Role = "Terminated";
        //        _db.SaveChanges();
        //    }
        //    if (role == "Case Manager")
        //        return RedirectToAction("ManageCaseManagerCases");
        //    else if (role == "Police Officer")
        //        return RedirectToAction("ManagePoliceOfficerCases");
        //    return RedirectToAction("Index");
        //}


    }
}
