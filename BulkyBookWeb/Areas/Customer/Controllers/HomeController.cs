using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BulkyBook.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly IUnitOfWork _iunitOfWork;
        private readonly ApplicationDbContext _context;
      

        public HomeController(ILogger<HomeController> logger, IUnitOfWork iunitOfWork, ApplicationDbContext context)
        {
            _logger = logger;
          
            _context = context;
            _iunitOfWork = iunitOfWork;

            
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _iunitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }
        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _iunitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType"),

            };
            return View(cartObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}