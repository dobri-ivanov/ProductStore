using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.DataAcess.Repository.Interfaces;
using ProductStore.Models;
using ProductStore.Models.ViewModels;
using System.Security.AccessControl;
using System.Security.Claims;

namespace ProductStore.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
		{
			var claims = (ClaimsIdentity)User.Identity;
			var userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

			IEnumerable<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(s => s.ApplicationUserId == userId, includeProperties: "Product");

			foreach (var sc in shoppingCarts)
			{
				sc.Product.Price = GetProductPrice(sc);
			}

			ShoppingCartVM sh = new ShoppingCartVM()
			{
				ShoppingCartList = shoppingCarts
            };
			return View(sh);
		}

		public IActionResult Minus(int cardId)
		{
			ShoppingCart shoppingCart = _unitOfWork.ShoppingCart.Get(s => s.Id == cardId);

			if (shoppingCart.Count == 1)
			{
				Delete(cardId);
			}
			else
			{
				shoppingCart.Count--;
				_unitOfWork.ShoppingCart.Update(shoppingCart);
				_unitOfWork.Save();
			}		

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Plus(int cardId)
		{
			ShoppingCart shoppingCart = _unitOfWork.ShoppingCart.Get(s => s.Id == cardId);
			shoppingCart.Count++;
			_unitOfWork.ShoppingCart.Update(shoppingCart);
			_unitOfWork.Save();

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int cardId)
		{
			ShoppingCart shoppingCart = _unitOfWork.ShoppingCart.Get(s => s.Id == cardId);
			_unitOfWork.ShoppingCart.Remove(shoppingCart);
			_unitOfWork.Save();

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Summary()
		{
			return View();
		}

		private double GetProductPrice(ShoppingCart shoppingCart)
		{
			if (shoppingCart.Count > 100)
			{
				return shoppingCart.Product.Price100;
			}
			else if (shoppingCart.Count > 50)
			{
				return shoppingCart.Product.Price50;
			}
			else 
			{
				return shoppingCart.Product.Price;
			}
		}
	}
}
