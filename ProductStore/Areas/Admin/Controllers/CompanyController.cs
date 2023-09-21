using Microsoft.AspNetCore.Authorization;
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
	public class CompanyController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CompanyController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Upsert(int? id)
		{

			if (id == null || id == 0)
			{
				return View(new Company());
			}
			else
			{
				Company company = _unitOfWork.Company.Get(p => p.Id == id);
				return View(company);
			}
		}

		[HttpPost]
		public IActionResult Upsert(Company obj)
		{

			if (ModelState.IsValid)
			{
				if (obj.Id == 0)
				{
					_unitOfWork.Company.Add(obj);
					TempData["success"] = "A new Company is successfully created!";
				}
				else
				{
					_unitOfWork.Company.Update(obj);
					TempData["success"] = "The company is successfully updated!";
				}

				_unitOfWork.Save();


				return RedirectToAction("Index");
			}

			return View(obj);

		}
		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Company> companies = _unitOfWork.Company.GetAll().ToList();
			return Json(new { data = companies });
		}

		public IActionResult Delete(int? id)
		{
			var company = _unitOfWork.Company.Get(p => p.Id == id);

			if (company == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}

			_unitOfWork.Company.Remove(company);
			_unitOfWork.Save();

			return Json(new { success = true, message = "Deleted Successful" });
		}

		#endregion

	}
}
