using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAP.Data;
using SAP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Controllers
{
    public class CaseManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CaseManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SelectManager
        public IActionResult SelectManager(int caseId)
        {
            var caseToAllocate = _context.Cases_Table.Find(caseId);
            if (caseToAllocate == null)
            {
                return NotFound();
            }

            // Fetch eligible managers (those with the lowest case count)
            var managers = _context.Managers_Table.ToList();
            var minCaseCount = managers.Min(m => m.CaseCount);
            var eligibleManagers = managers.Where(m => m.CaseCount == minCaseCount).ToList();

            if (!eligibleManagers.Any())
            {
                return NotFound(); // No eligible managers found
            }

            // Pass the case and eligible managers to the view
            ViewBag.CaseToAllocate = caseToAllocate;
            return View(eligibleManagers);
        }

        // POST: AllocateSelectedManager
        [HttpPost]
        public async Task<IActionResult> AllocateSelectedManager(int caseId, string selectedManagerId)
        {
            var caseToAllocate = await _context.Cases_Table.FindAsync(caseId);
            var selectedManager = await _context.Managers_Table.FindAsync(selectedManagerId);

            if (caseToAllocate == null || selectedManager == null)
            {
                return NotFound();
            }

            // Assign the case to the selected manager
            caseToAllocate.AssignedTo = selectedManager.First_Name + " " + selectedManager.Last_Name;
            caseToAllocate.ManagerId = selectedManager.ManagerId;
            caseToAllocate.IsAssigned = true;
            caseToAllocate.CaseStatus = "In-Progress";

            // Update manager's case count
            selectedManager.CaseCount++;

            _context.Update(caseToAllocate);
            _context.Update(selectedManager);
            await _context.SaveChangesAsync();

            return RedirectToAction("CaseManager", "AllocateCases"); // Redirect back to the case allocation page
        }

    }
}
