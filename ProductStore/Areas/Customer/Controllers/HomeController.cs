using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.DataAcess.Repository;
using ProductStore.DataAcess.Repository.Interfaces;
using ProductStore.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ProductStore.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category");
			return View(products);
		}
		public IActionResult Details(int productId)
		{

			ShoppingCart shoppingCart = new ShoppingCart()
			{
				Product = _unitOfWork.Product.Get(p => p.Id == productId, includeProperties: "Category"),
				ProductId = productId,
				Count = 1
			};

			return View(shoppingCart);
		}

		[HttpPost]
		[Authorize]
		public IActionResult Details(ShoppingCart shoppingCart)
		{
			var claims = (ClaimsIdentity)User.Identity;
			var userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

			shoppingCart.ApplicationUserId = userId;

			var cartFormDb = _unitOfWork.ShoppingCart.Get(s => s.ProductId == shoppingCart.ProductId && s.ApplicationUserId == userId);

			if (cartFormDb != null)
			{
				cartFormDb.Count += shoppingCart.Count;
				_unitOfWork.ShoppingCart.Update(cartFormDb);
			}
			else
			{
				_unitOfWork.ShoppingCart.Add(shoppingCart);
			}

			_unitOfWork.Save();
			return RedirectToAction("Index");
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