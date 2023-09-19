using Microsoft.AspNetCore.Mvc;
using ProductStore.DataAcess.Data;
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

			if (!ModelState.IsValid)
			{
				return View(obj);
			}

			_data.Categories.Add(obj);
			_data.SaveChangesAsync();

			TempData["success"] = "A new category is successfully created!";
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category? obj = _data.Categories.Find(id);

			if (obj == null)
			{
				return NotFound();
			}

			return View(obj);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (obj == null)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(obj);
			}

			_data.Categories.Update(obj);
			_data.SaveChanges();

			TempData["success"] = "Category is successfully updated!";
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category? obj = _data.Categories.Find(id);

			if (obj == null)
			{
				return NotFound();
			}

			return View(obj);
		}

		[HttpPost]
		public IActionResult Delete(Category obj)
		{
			if (obj == null)
			{
				return NotFound();
			}

			_data.Categories.Remove(obj);
			_data.SaveChanges();

			TempData["success"] = "Category is successfully deleted!";
			return RedirectToAction("Index");
		}
	}
}
