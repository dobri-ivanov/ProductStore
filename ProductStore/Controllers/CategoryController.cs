using Microsoft.AspNetCore.Mvc;
using ProductStore.DataAcess.Data;
using ProductStore.DataAcess.Repository.Interfaces;
using ProductStore.Models;

namespace ProductStore.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult Index()
		{
			List<Category> categories = _unitOfWork.CategoryRepository.GetAll().OrderBy(c => c.DisplayOrder).ToList();
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

			_unitOfWork.CategoryRepository.Add(obj);
			_unitOfWork.Save();

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

			Category? obj = _unitOfWork.CategoryRepository.Get(obj => obj.Id == id);

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

			_unitOfWork.CategoryRepository.Update(obj);
			_unitOfWork.Save();

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

			Category? obj = _unitOfWork.CategoryRepository.Get(obj => obj.Id == id);

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

			_unitOfWork.CategoryRepository.Remove(obj);
			_unitOfWork.Save();

			TempData["success"] = "Category is successfully deleted!";
			return RedirectToAction("Index");
		}
	}
}
