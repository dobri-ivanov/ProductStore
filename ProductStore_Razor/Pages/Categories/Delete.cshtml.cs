using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductStore_Razor.Data;
using ProductStore_Razor.Models;

namespace ProductStore_Razor.Pages.Categories
{
	public class DeleteModel : PageModel
	{
		private readonly ProductStoreDbContext _data;

		[BindProperty]
		public Category? Category { get; set; } = null!;

		public DeleteModel(ProductStoreDbContext data)
		{
			_data = data;
		}
		public void OnGet(int? id)
		{
			if (id != null && id != 0)
			{
				Category = _data.Categories.Find(id);
			}
		}

		public IActionResult OnPost(Category obj)
		{
			if (obj != null)
			{
				_data.Categories.Remove(obj);
				_data.SaveChanges();
			}

			return RedirectToPage("Index");
		}
	}
}
