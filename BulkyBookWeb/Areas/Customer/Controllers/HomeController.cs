using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BulkyBook.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

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

            _iunitOfWork = iunitOfWork;
            _context = context;


        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _iunitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cartObj = new ()     
            {
                Count = 1,
               ProductId = productId,
                Product = _iunitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType"),

            };
            return View(cartObj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]//only authorize user access
        public IActionResult Details(ShoppingCart shoppingCart)
        {
           //user identity
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            //checking for existing cart
            ShoppingCart cartFromDb = _iunitOfWork.ShoppingCart.GetFirstOrDefault(
                u=> u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb == null)
            {
                _iunitOfWork.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                _iunitOfWork.ShoppingCart.IncrementCount(cartFromDb,shoppingCart.Count);//updated
            }
            _iunitOfWork.Save();
            return RedirectToAction(nameof(Index));

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