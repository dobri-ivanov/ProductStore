using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductStore_Razor.Data;
using ProductStore_Razor.Models;

namespace ProductStore_Razor.Pages.Categories
{
    
    public class CreateModel : PageModel
    {
		private readonly ApplicationDbContext _data;

        [BindProperty]
        public Category Category { get; set; } = null!;

        public CreateModel(ApplicationDbContext data)
        {
            _data = data;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()

        {
            
            _data.Categories.Add(Category);
            _data.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
