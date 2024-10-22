using Microsoft.AspNetCore.Mvc;
using SAP.Data;
using SAP.Models;

namespace SAP.Controllers
{
    public class AssignCaseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AssignCaseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //-----------------------------------------------------------------------------
        

   
        //private List<Cases> cases = new List<Cases>();

        // GET: Case/Allocate
        public ActionResult AllocateCase()
        {
            return View(_db.Managers_Table.ToList());
        }

        [HttpPost]
        public ActionResult AllocateCase(Cases newCase)
        {
            // Find managers with the minimum case count
            var minCases = _db.Managers_Table.ToList().Min(m => m.CaseCount);
            var eligibleManagers = _db.Managers_Table.ToList().Where(m => m.CaseCount == minCases).ToList();

            if (eligibleManagers.Count == 1)
            {
                // Allocate to the manager with the lowest case count
                newCase.ManagerId = eligibleManagers.First().Id.ToString();
                eligibleManagers.First().CaseCount++;
            }
            else
            {
                // More than one eligible manager, show selection view
                return View("SelectManager", eligibleManagers);
            }

            
            return RedirectToAction("Index"); // Redirect to some index view after allocation
        }

        //[HttpPost]
        //public ActionResult SelectManager(int managerId, Cases newCase)
        //{
        //    // Allocate to the selected manager
        //    newCase.ManagerId = Convert.ToInt16(managerId).ToString();
        //    var manager = _db.Managers_Table.ToList().First(m => m.Id == managerId);
        //    manager.CaseCount++;

        //    cases.Add(newCase);
        //    return RedirectToAction("Index"); // Redirect to some index view after allocation
        //}

    }
}
