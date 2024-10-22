using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAP.Data;
using System.Dynamic;
using System.Linq;

namespace SAP.Controllers
{
    public class SearchCriminalController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SearchCriminalController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchById(string searchString, string search)
        {
            //ViewData["CurrentFilter"] = searchString;

            var criminal_record = from r in _db.CriminalRecords_Table
                                  select r;
                                                                                                                                     
            var memberList = _db.CriminalRecords_Table.Where(m=> m.CriminalIdNum.Contains(search));
            var SuspectInfo = _db.Suspects_Table.Where(m => m.SuspectIdNum.Contains(search));

            dynamic FlexModel = new ExpandoObject();
            FlexModel.CriminalRecords_Table = memberList;
            FlexModel.Suspects_Table = SuspectInfo;

            return View(FlexModel);
        }

    }
}
