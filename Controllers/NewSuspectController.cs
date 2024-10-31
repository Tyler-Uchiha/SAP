using SAP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Remoting;
using Microsoft.EntityFrameworkCore;
using SAP.Data;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;

namespace SAP.Controllers
{
    public class NewSuspectController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NewSuspectController(ApplicationDbContext db)
        {
            _db = db;
        }

        private IEnumerable<SelectListItem> GetDropdownSex()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Male" },
                new SelectListItem { Text = "Female" }
            };
        }

        
        private IEnumerable<SelectListItem> GetDropdownEyes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Brown" },
                new SelectListItem { Text = "Blue" },
                new SelectListItem { Text = "Green" },
                new SelectListItem { Text = "Gray" }
            };
        }

        public IActionResult RecordNewSuspect()
        {
            if (User.IsInRole("Superman") ^ User.IsInRole("Police Officer"))
            {
                ViewBag.Sex = GetDropdownSex();
                ViewBag.EyeColour = GetDropdownEyes();

                return View();
            }
            else
            {
                return RedirectToAction("NoEntry", "Home");
            }
        }

        [HttpPost]        
        public IActionResult RecordNewSuspect(NewSuspect obj, ListofOffences thing) 
        {
            

            //Repopulate dropdown 
            ViewBag.Sex = GetDropdownSex();
            ViewBag.EyeColour = GetDropdownEyes();

            bool blnValidInput = true;

            if (obj.SuspectIdNum != null)
            {
                string DateofBirth = "";

                // DateofBirth = obj.SuspectId.Substring(0, 6);

                for (int i = 0; i < 6; i++)
                {
                    DateofBirth += obj.SuspectIdNum[i];
                }

                int DateofBirth_yy = Convert.ToUInt16(DateofBirth[0].ToString() + DateofBirth[1].ToString());

                //Convert.ToInt32(DateofBirth);
                string curr_year;
                string str_temp = "";
                int min_year, age;
                int curr_year_yy;
                const int min_year_const = 18;

                curr_year = DateTime.Now.Year.ToString();
                str_temp = curr_year[2].ToString() + curr_year[3].ToString();
                curr_year_yy = Convert.ToInt16(str_temp);
                min_year = curr_year_yy - min_year_const;

                //Det Age 
                
                if (DateofBirth_yy >= 0 && DateofBirth_yy <= min_year)
                {
                    int Year = 2000 + DateofBirth_yy;
                    obj.Age = DateTime.Now.Year - Year;
                }
                else
                {
                   int Year = 1900 + DateofBirth_yy;
                   age = DateTime.Now.Year - Year;
                   obj.Age = age;
                }


                //For 2000's
                if ((Convert.ToInt32(DateofBirth[0].ToString() + DateofBirth[1].ToString())) > min_year && (Convert.ToInt32(DateofBirth[0].ToString() + DateofBirth[1].ToString())) < 0)
                {
                    ModelState.AddModelError("SuspectIdNum", "Invalid year 1");
                    blnValidInput = false;
                }
                //Born before 2000 and valid if born between 1914 and 1999
                else if (((Convert.ToInt32(DateofBirth[0].ToString() + DateofBirth[1].ToString())) < 14 || (Convert.ToInt32(DateofBirth[0].ToString() + DateofBirth[1].ToString())) > 99) && obj.Age > curr_year_yy)
                {
                    ModelState.AddModelError("SuspectIdNum", "Invalid year 2");
                    blnValidInput = false;
                }
                else if ((Convert.ToInt32(DateofBirth[2].ToString() + DateofBirth[3].ToString())) < 1 || (Convert.ToInt32(DateofBirth[2].ToString() + DateofBirth[3].ToString())) > 12)
                {
                    ModelState.AddModelError("SuspectIdNum", "Invalid month");
                    blnValidInput = false;
                }
                else if ((Convert.ToInt32(DateofBirth[4].ToString() + DateofBirth[5].ToString())) < 1 || (Convert.ToInt32(DateofBirth[4].ToString() + DateofBirth[5].ToString())) > 31)
                {
                    ModelState.AddModelError("SuspectIdNum", "Invalid day");
                    blnValidInput = false;
                }

                //Generate SuspectId

                var lastId = _db.Suspects_Table
                .FromSql($"SELECT Max(Id) AS Id FROM [dbo].[Suspects_Table]")
                .Select(s => s.Id)
                .FirstOrDefault();

                int MaxId = Convert.ToInt32(lastId) + 1;

                obj.SuspectId = "S" + "-" + obj.SuspectIdNum.Substring(0, 6) + "-" + DateTime.Now.Year.ToString() + "-" + MaxId.ToString();

                //Input Validation - Failsafe

                if (blnValidInput == true && obj != null && ModelState.IsValid)
                {
        
                    _db.Suspects_Table.Update(obj);
                    _db.SaveChanges();
                    TempData["AlertMessage"] = "Suspect Added";
                }
                else 
                {   
                    //Add toast to indicate unncessful state
                    return View();
                }
            }
            
            return RedirectToAction("TableAllSuspects");
        }

        //Display all suspects
        public IActionResult TableAllSuspects() 
        { 
            var suspects = _db.Suspects_Table.ToList();
            return View(suspects);
        }

        //Edit 
        public IActionResult Edit(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            //Fetch record

            NewSuspect DataRow = _db.Suspects_Table.Find(Id);

            if (DataRow == null)
            {
                return NotFound();
            }

            return View(DataRow);
        }

        [HttpPost]
        public IActionResult Edit(NewSuspect obj)
        {

            if (ModelState.IsValid)
            {
                _db.Suspects_Table.Update(obj);
                _db.SaveChanges();
                TempData["AlertMessage_Edit"] = "Edited Record Saved";
                return RedirectToAction("TableAllSuspects", "NewSuspect");
            }
            else
            {
                //This to repopulate the list after an unsuccessful post
                TempData["AlertMessage_Error"] = "Edited Record Not Saved";
                return View();
            }

        }
        //Archive then delete obj

        //display deleted records
    }
}
