using BulkyBook.DataAccess.Data;
using BulkyBook.Model;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PlayerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Player> objCategoryList = _db.Players;//retrive all datan asign
            return View(objCategoryList);
        }
    }
}
