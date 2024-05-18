using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaWeb.DataAccess.Repository;
using PizzaWeb.DataAccess.Repository.IRepository;
using PizzaWeb.Models;
using PizzaWeb.Models.ViewModels;
using PizzaWeb.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace PizzaWeb.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            IEnumerable<Product> productList = _unitOfWork.Product.GetALl();
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            //if (User.Identity.IsAuthenticated)
            //{

            //    Product product = _unitOfWork.Product.Get(x => x.Id == productId, includeProperties: "Category");
            //    return View(product);
            //}
            //else
            //{

            //    return RedirectToAction("AccessDenied", "Identity");

            //}

            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(x => x.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

		[HttpPost]
		[Authorize]
		public IActionResult Details(ShoppingCart shoppingCart)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			shoppingCart.ApplicationUserId = userId;

			// Check if a cart item with the same product ID already exists for the user
			var cartFromDb = _unitOfWork.ShoppingCart.Get(x => x.ApplicationUserId == userId && x.ProductId == shoppingCart.ProductId);

			if (cartFromDb != null)
			{
				//Update the existing cart item with the new count
				cartFromDb.Count = 0;
				cartFromDb.Count += shoppingCart.Count;
				_unitOfWork.ShoppingCart.Update(cartFromDb);
				shoppingCart = cartFromDb;

				//_unitOfWork.ShoppingCart.Add(shoppingCart);

			}
			else
			{
				// If the cart item doesn't exist, add it to the database
				_unitOfWork.ShoppingCart.Add(shoppingCart);
			}

			_unitOfWork.Save();

			// Redirect to the OrderDetails action with the ID of the newly created or updated cart item
			return RedirectToAction(nameof(OrderDetails), new { cartItemId = shoppingCart.Id });
		}




		//     [HttpPost]
		//     [Authorize]
		//     public IActionResult Details(ShoppingCart shoppingCart)
		//     {
		//         var claimsIdentity = (ClaimsIdentity)User.Identity;
		//         var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

		//         shoppingCart.ApplicationUserId = userId;
		//var cartFromDb = _unitOfWork.ShoppingCart.Get(x => x.ApplicationUserId == userId && x.ProductId == shoppingCart.ProductId);

		//if (cartFromDb == null)
		//{
		//	// Handle the case where the cart item could not be found
		//	return NotFound();
		//}

		//int Count = shoppingCart.Count;
		//         _unitOfWork.ShoppingCart.Add(shoppingCart);
		//         _unitOfWork.Save();

		//         return RedirectToAction(nameof(OrderDetails), new { cartItemId = cartFromDb.Id });
		//     }


		//public IActionResult OrderDetails()
		//{
		//    var claimsIdentity = (ClaimsIdentity)User.Identity;
		//    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

		//    //IEnumerable<Product> productList = _unitOfWork.Product.GetALl();
		//    // IEnumerable<ApplicationUser> applications = _unitOfWork..GetALl();

		//    ShoppingCartVM = new()
		//    {
		//        ShoppingCartList = _unitOfWork.ShoppingCart.GetALl(x => x.ApplicationUserId == userId,
		//         includeProperties: "Product"),

		//        OrderHeader = new()
		//    };

		//    ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(x => x.Id == userId);
		//    //ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.

		//    ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.UserName;
		//    ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
		//    ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
		//    ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
		//    ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
		//    ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
		//    //ShoppingCartVM.OrderHeader.Quantity = ShoppingCartVM.ShoppingCartt.Count;


		//    return View(ShoppingCartVM);

		//}


		[HttpGet("OrderDetails/{cartItemId}")]
		public IActionResult OrderDetails(int cartItemId)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			var shoppingCartItem = _unitOfWork.ShoppingCart.Get(
				x => x.Id == cartItemId && x.ApplicationUserId == userId,
				includeProperties: "Product"
			);

			if (shoppingCartItem == null)
			{
				return NotFound();
			}


                // Set the price from the product's price
    shoppingCartItem.Price = shoppingCartItem.Product.Price100;


			var applicationUser = _unitOfWork.ApplicationUser.Get(x => x.Id == userId);

			var shoppingCartVM = new ShoppingCartVM
			{
				ShoppingCartItem = shoppingCartItem,
				OrderHeader = new OrderHeader()
			};

			if (applicationUser != null)
			{
				shoppingCartVM.OrderHeader.ApplicationUser = applicationUser;
				shoppingCartVM.OrderHeader.Name = applicationUser.Name;
				shoppingCartVM.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
				shoppingCartVM.OrderHeader.StreetAddress = applicationUser.StreetAddress;
				shoppingCartVM.OrderHeader.City = applicationUser.City;
				shoppingCartVM.OrderHeader.State = applicationUser.State;
				shoppingCartVM.OrderHeader.PostalCode = applicationUser.PostalCode;
			}

			return View(shoppingCartVM);
		}

        [HttpPost]
        public IActionResult OrderNow(ShoppingCartVM shoppingCartVM)
        {

			if (ModelState.IsValid)
			{
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

               // var shoppingCartItem = _unitOfWork.ShoppingCart.Include(x => x.Product)
              //  .FirstOrDefault(x => x.Id == shoppingCartVM.ShoppingCartItem.Id && x.ApplicationUserId == userId);

                //if (shoppingCartItem == null)
                //{
                //    return NotFound();
                //}

                var orderHeader = new OrderHeader
                {
                    ApplicationUserId = userId,
                    Name = shoppingCartVM.OrderHeader.Name,
                    PhoneNumber = shoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber,
                    StreetAddress = shoppingCartVM.OrderHeader.StreetAddress,
                    City = shoppingCartVM.OrderHeader.City,
                    State = shoppingCartVM.OrderHeader.State,
                    PostalCode = shoppingCartVM.OrderHeader.PostalCode,
                    OrderDate = DateTime.Now,
                   // OrderTotal = shoppingCartVM.ShoppingCartItem.Count * shoppingCartItem.Product.Price100 // Assuming Price100 is the correct property for price
                };
                _unitOfWork.OrderHeaderRepository.Add(orderHeader);
                _unitOfWork.Save();


            }

            return View("Index");
        }















        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult AboutUs()
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
