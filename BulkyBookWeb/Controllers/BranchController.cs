using BulkyBook.DataAccess.Data;
using BulkyBook.Model;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BranchController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Branch> objCategoryList = _db.Branches;//retrive all datan asign
            return View(objCategoryList);
        }
        
    }
}
