using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _data.Categories.ToListAsync();
            return View(categories);
        }
    }
}
