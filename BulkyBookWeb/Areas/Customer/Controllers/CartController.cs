using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBokkWeb.Areas.Customer.Contollers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _iunitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
       
        public CartController(IUnitOfWork unitOfWork)
        {
            _iunitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            //get identity
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _iunitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"), //get list 
                OrderHeader = new()
            };

            //price50 100
            foreach(var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50,cart.Product.Price100);

                ShoppingCartVM.OrderHeader.OrderTotal+= (cart.Price * cart.Count);//cartTotal
            }
            return View(ShoppingCartVM);
        }
        public IActionResult Summary()
        {
            //get identity
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _iunitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"), //get list 
                OrderHeader = new() //no give errorno object
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _iunitOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == claim.Value);//retive all user details

            //populate all details in order header
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;


           // price50 100
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50, cart.Product.Price100);

                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);//cartTotal
            }
			return View(ShoppingCartVM);
		
        }

        //increase qnty
        public IActionResult Plus(int cartId)
        {
            var cart = _iunitOfWork.ShoppingCart.GetFirstOrDefault(u=> u.Id == cartId);
            _iunitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _iunitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cart = _iunitOfWork.ShoppingCart.GetFirstOrDefault(u=> u.Id == cartId);
            if (cart.Count <=1)
            {
              
                _iunitOfWork.ShoppingCart.Remove(cart);

            }
            else {
                _iunitOfWork.ShoppingCart.DecrementCount(cart, 1);
              
            }
            _iunitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _iunitOfWork.ShoppingCart.GetFirstOrDefault(u=> u.Id == cartId);
            _iunitOfWork.ShoppingCart.Remove(cart);
            _iunitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        //automatically decrease price
        private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else
            {
                if (quantity <= 100)
                {
                    return price50;
                }
                return price100;
            }

        }
    }
}