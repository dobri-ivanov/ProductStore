﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductStore.DataAcess.Data;
using ProductStore.DataAcess.Repository.Interfaces;
using ProductStore.Models;
using ProductStore.Models.ViewModels;
using ProductStore.Utility;

namespace ProductStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Authorize(Roles = SD.Role_Admin)]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Upsert(int? id)
		{

			ProductVM productVM = new ProductVM()
			{
				Product = new Product(),
				CategoryList = _unitOfWork.Category
				 .GetAll().Select(u => new SelectListItem
				 {
					 Text = u.Name,
					 Value = u.Id.ToString()
				 })
			};

			if (id == null || id == 0)
			{

				return View(productVM);
			}
			else
			{
				productVM.Product = _unitOfWork.Product.Get(p => p.Id == id);
				return View(productVM);
			}
		}

		[HttpPost]
		public IActionResult Upsert(ProductVM obj, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");

					if (!String.IsNullOrEmpty(obj.Product.ImageUrl))
					{
						var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));

						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					obj.Product.ImageUrl = @"\images\product\" + fileName;
				}

				if (obj.Product.Id == 0)
				{
				_unitOfWork.Product.Add(obj.Product);
				}
				else
				{
					_unitOfWork.Product.Update(obj.Product);
				}

				_unitOfWork.Save();

				TempData["success"] = "A new Product is successfully created!";
				return RedirectToAction("Index");
			}

			obj.CategoryList = _unitOfWork.Category
				 .GetAll().Select(u => new SelectListItem
				 {
					 Text = u.Name,
					 Value = u.Id.ToString()
				 });

			return View(obj);

		}
		#region API CALLS
		[HttpGet]
		public IActionResult GetAll() 
		{
			List<Product> categories = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return Json(new { data = categories });
		}

		public IActionResult Delete(int? id)
		{
			var product = _unitOfWork.Product.Get(p => p.Id == id);

			if (product == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}

			var oldImageUrl = 
				Path.Combine(_webHostEnvironment.WebRootPath,
				product.ImageUrl.TrimStart('\\'));

			if (System.IO.File.Exists(oldImageUrl))
			{
				System.IO.File.Delete(oldImageUrl);
			}

			_unitOfWork.Product.Remove(product); 
			_unitOfWork.Save();

			return Json(new { success = true, message = "Deleted Successful" });
		}

		#endregion

	}
}
