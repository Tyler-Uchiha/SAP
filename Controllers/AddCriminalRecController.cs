using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SAP.Data;
using SAP.Models;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SAP.Controllers
{
    
    public class AddCriminalRecController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _db = context;

        public IActionResult Index()
        {
            return View();
        }

        private void GetCurrentIssuedDate(AddCriminalRec thing)
        {
            ViewBag.CurrIssueDate = thing.IssueDate;
        }

        private IEnumerable<SelectListItem> GetStations()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Midrand" },
                new SelectListItem { Text = "Rosebank" },
                new SelectListItem { Text = "Protea Glen" },
                new SelectListItem { Text = "Durban" },
                new SelectListItem { Text = "Cape Town" },
                new SelectListItem { Text = "Pretoria" },
                new SelectListItem { Text = "Sandton" },
                new SelectListItem { Text = "Braamfontein" },
                new SelectListItem { Text = "Court" },
                new SelectListItem { Text = "Other" }
            };
        }

        private IEnumerable<SelectListItem> GetCaseStatus()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Not Started" },
                new SelectListItem { Text = "In-Progress" },
                new SelectListItem { Text = "Resolved" },
                new SelectListItem { Text = "Other" }
            };
        }
        private IEnumerable<SelectListItem> GetOffences()
        {
            var offences = from r in _db.Offences_Table
                           select r.Offences.ToString();

            List<SelectListItem> offenceItems = new List<SelectListItem>();

            foreach (var thing in offences)
            {
                offenceItems.Add(new SelectListItem
                {
                    Value = thing,
                    Text = thing
                });
            }

            return offenceItems;
        }


        public IActionResult AddCriminalRec(string suspectIdNum, string First_Name, string Last_Name, int Age)
        {
            
            TempData["TmpSuspectIdNum"] = suspectIdNum;
            TempData["Age"] = Age;

            ViewBag.Stations = GetStations();
            ViewBag.OffenceList = GetOffences();
            return View();
        }

        [HttpPost]
        public IActionResult AddCriminalRec(AddCriminalRec item, Cases obj)
        {
            ViewBag.Stations = GetStations();
            ViewBag.OffenceList = GetOffences();

            string StrIssueDateUnsorted = item.IssueDate.Substring(0, 10);
            string StrIssueDateSorted = "";
            string suspectIdNum = TempData["TmpSuspectIdNum"] as string;

            //Add the criminal's Id number into the criminal record table
            item.CriminalIdNum = suspectIdNum;


            for (int i = 0; i < StrIssueDateUnsorted.Length; i++)
            {
                if (StrIssueDateUnsorted[i] != '-')
                {
                    StrIssueDateSorted += StrIssueDateUnsorted[i];
                }
            }

            var lastId = _db.CriminalRecords_Table
                .FromSql($"SELECT Max(Id) AS Id FROM [dbo].[CriminalRecords_Table]")
                .Select(s => s.Id)
                .FirstOrDefault();

            int MaxId = Convert.ToInt32(lastId) + 1;

            //Auto Generate Criminal record Id
            item.CriminalRecordId = "C" + "-" + suspectIdNum.Substring(0, 6) + "-" + StrIssueDateSorted + "-" + MaxId;

            if (item != null && ModelState.IsValid)  
            {
                //Add criminal record to cases
                obj.CriminalRecordId = item.CriminalRecordId;
                obj.CriminalIdNum = item.CriminalIdNum;
                obj.OffenceCommitted = item.OffenceCommitted;
                obj.Sentence = item.Sentence;
                obj.IssuedAt = item.IssuedAt;
                obj.IssuedBy = item.IssuedBy;
                obj.IssueDate = item.IssueDate;
                obj.IsAssigned = false;
                obj.AssignedTo = "None";
                obj.ManagerId = "None";
                obj.CaseStatus = "Not Started";
                

                //============================================================
                // Find eligible managers with the role "Case Manager"
                var managers = _db.Users.Where(u => u.User_Role == "Case Manager").ToList();
                var minCaseCount = managers.Min(m => m.CaseCount);
                var eligibleManagers = managers.Where(m => m.CaseCount == minCaseCount).ToList();

                if (eligibleManagers.Count >= 1)
                {
                    // Automatically assign to the single eligible manager
                    var selectedManager = eligibleManagers.First();
                    obj.AssignedTo = selectedManager.First_Name + " " + selectedManager.Last_Name;
                    obj.ManagerId = selectedManager.Id; // Assuming Id is the primary key
                    obj.IsAssigned = true;
                    obj.CaseStatus = "In-Progress";

                    // Update manager's case count
                    selectedManager.CaseCount++;
                    _db.Users.Update(selectedManager);
                }
                else if (eligibleManagers.Count == 0)
                {
                    obj.IsAssigned = false;
                    obj.CaseStatus = "Not Assigned";
                    item.Case_Status = "Not Started";
                }

                // Save changes
                _db.SaveChangesAsync().GetAwaiter().GetResult();


                //============================================================

                ////-->
                //// Find eligible managers
                //var managers = _db.Managers_Table.ToList();
                //var minCaseCount = managers.Min(m => m.CaseCount);
                //var eligibleManagers = managers.Where(m => m.CaseCount == minCaseCount).ToList();

                //if (eligibleManagers.Count >= 1)
                //{
                //    // Automatically assign to the single eligible manager
                //    var selectedManager = eligibleManagers.First();
                //    obj.AssignedTo = selectedManager.First_Name + " " + selectedManager.Last_Name;
                //    obj.ManagerId = selectedManager.ManagerId;
                //    obj.IsAssigned = true;
                //    obj.CaseStatus = "In-Progress";

                //    // Update manager's case count
                //    selectedManager.CaseCount++;
                //    _db.Managers_Table.Update(selectedManager);
                //}
                //else if (eligibleManagers.Count == 0)
                //{
                //    obj.IsAssigned = false; 
                //    obj.CaseStatus = "Not Assigned";
                //}

                // <--
                _db.CriminalRecords_Table.Update(item);
                _db.Cases_Table.Update(obj);
                item.IssueDate = item.IssueDate.Substring(0, 10);
                _db.SaveChanges();
                TempData["AlertMessage"] = "Criminal Record Added";

                return RedirectToAction("AllCriminalRecords", "AddCriminalRec");
            }
            else
            {
                TempData["AlertMessage_Error"] = "Criminal Record Not Added";
                return View();
            }
        }

        //Add FullName to the criminal record
        //Add view to display all the criminal records
        public IActionResult AllCriminalRecords()
        { 
            var AllCriminals = _db.CriminalRecords_Table.ToList();  
            return View(AllCriminals);
        }

        //Edit record
        //Pass the captured date 

        //private void PopulateOffencesList()
        //{
        //    var offenceList = from r in _db.Offences_Table
        //                      select r.Offences;

        //    List<string> offencesList_tmp = offenceList.ToList();

        //    ViewBag.OffencesList = new SelectList(offencesList_tmp);
        //}

        

        public IActionResult Edit(int Id, AddCriminalRec obj)
        {
            
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            //Fetch record

            AddCriminalRec DataRow = _db.CriminalRecords_Table.Find(Id);

            

            if (DataRow == null)
            { 
                return NotFound();
            }

            //PopulateOffencesList();
            ViewBag.OffencesList = GetOffences();
            ViewBag.Case_Status = GetCaseStatus();  
            GetCurrentIssuedDate(DataRow);

            return View(DataRow);
        }

        [HttpPost]
        public IActionResult Edit(AddCriminalRec obj)
        {
            // Populate dropdown lists
            ViewBag.OffencesList = GetOffences();
            ViewBag.Case_Status = GetCaseStatus();
            GetCurrentIssuedDate(obj);

            if (ModelState.IsValid)
            {
                // Update Criminal Records Table
                _db.CriminalRecords_Table.Update(obj);
                _db.SaveChanges();

                // Fetch the user associated with this case
                var user = _db.Cases_Table.FirstOrDefault(u => u.CriminalRecordId == obj.CriminalRecordId);

                if (user != null)
                {
                    // Update the user's case status
                    user.CaseStatus = obj.Case_Status;  // Assuming CaseStatus is a property in your ApplicationUser model

                    //// Save the changes to the user

                    _db.SaveChanges();
                }

                return RedirectToAction("TableAllSuspects", "NewSuspect");
            }
            else
            {
                // This is to repopulate the list after an unsuccessful post
                ViewBag.OffencesList = GetOffences();
                GetCurrentIssuedDate(obj);
                TempData["AlertMessage_Edit_Error"] = "Edit Unsuccessful";
                return View(obj);
            }
        }

        //[HttpPost]
        //public IActionResult Edit(AddCriminalRec obj)
        //{
        //    //PopulateOffencesList();
        //    ViewBag.OffencesList = GetOffences();
        //    ViewBag.Case_Status = GetCaseStatus();
        //    GetCurrentIssuedDate(obj);

        //    if (ModelState.IsValid)
        //    {
        //        //_db.Cases_Table.Update()
        //        _db.CriminalRecords_Table.Update(obj);
        //        _db.SaveChanges();

        //        return RedirectToAction("TableAllSuspects", "NewSuspect");
        //    }
        //    else
        //    {
        //        //This to repopulate the list after an unsuccessful post
        //        //PopulateOffencesList();
        //        ViewBag.OffencesList = GetOffences();
        //        GetCurrentIssuedDate(obj);
        //        TempData["AlertMessage_Edit_Error"] = "Edit Unsuccessful";
        //        return View();
        //    }

        //}


        private void GetCaseManagerStats()
        {
            // Check if the current user has a role of "Case Manager"
            if (User.IsInRole("Case Manager"))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Retrieve the first name of the current user
                var firstName = _db.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.First_Name)
                    .FirstOrDefault();

                // Assign the first name to ViewBag
                ViewBag.FirstName = firstName;

                // Find all cases assigned to the current user
                var userCases = _db.Cases_Table.Where(c => c.ManagerId == userId).ToList();

                // Total cases assigned to the user
                var Total_Cases = userCases.Count();

                // Open cases with status "In-Progress"
                var Open_Cases = userCases.Count(c => c.CaseStatus == "In-Progress");

                // Closed cases with status "Closed"
                var Closed_Cases = userCases.Count(c => c.CaseStatus == "Resolved");

                // Assign case counts to ViewBag
                ViewBag.Total_Cases = Total_Cases;
                ViewBag.Open_Cases = Open_Cases;
                ViewBag.Closed_Cases = Closed_Cases;
            }
            else
            {
                ViewBag.FirstName = string.Empty;
                ViewBag.Total_Cases = 0;
                ViewBag.Open_Cases = 0;
                ViewBag.Closed_Cases = 0;
            }



            //// Check if the current user has a role of "Case Manager"
            //if (User.IsInRole("Case Manager"))
            //{
            //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //    // Find all cases assigned to the current user
            //    var userCases = _db.Cases_Table.Where(c => c.ManagerId == userId).ToList();

            //    // Total cases assigned to the user
            //    var Total_Cases = userCases.Count();

            //    // Open cases with status "In-Progress"
            //    var Open_Cases = userCases.Count(c => c.CaseStatus == "In-Progress");

            //    // Closed cases with status "Closed"
            //    var Closed_Cases = userCases.Count(c => c.CaseStatus == "Resolved");

            //    // Assign to ViewBag
            //    ViewBag.Total_Cases = Total_Cases;
            //    ViewBag.Open_Cases = Open_Cases;
            //    ViewBag.Closed_Cases = Closed_Cases;
            //}
            //else
            //{
            //    ViewBag.Total_Cases = 0;
            //    ViewBag.Open_Cases = 0;
            //    ViewBag.Closed_Cases = 0;
            //}
        }
        public List<Cases> GetUserCases()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var userCases = _db.Cases_Table.Where(c => c.ManagerId == userId).ToList();
                return userCases;
            }

            return new List<Cases>(); // or handle the case where userId is null appropriately
        }

        public IActionResult CaseManagerView() 
        {
            GetCaseManagerStats();
            var AssignedCases = GetUserCases();

            return View(AssignedCases);
        }

        public IActionResult StationManagerView()
        {
            

            return View();
        }

    }
}
                              