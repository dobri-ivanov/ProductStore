using Microsoft.AspNetCore.Mvc;
using ProductStore.Data;
using ProductStore.Models;

namespace ProductStore.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ProductStoreDbContext _data;

		public CategoryController(ProductStoreDbContext data)
		{
			_data = data;
		}

		[HttpGet]
		public IActionResult Index()
		{
			List<Category> categories = _data.Categories.ToList();
			return View(categories);
		}

		[HttpGet]
		public IActionResult Create()
		{			
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (obj == null)
			{
				return NotFound();
			}

			_data.Categories.Add(obj);
			_data.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}
