using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductStore_Razor.Data;
using ProductStore_Razor.Models;

namespace ProductStore_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
		private readonly ApplicationDbContext _data;

		[BindProperty]
		public Category? Category { get; set; }

		public EditModel(ApplicationDbContext data)
		{
			_data = data;
		}
		public void OnGet(int? id)
        {
			if (id != null || id != 0)
			{
				Category = _data.Categories.Find(id);
			}
        }

		
    }
}
	