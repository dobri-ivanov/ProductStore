#nullable disable
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductStore.Models.ViewModels
{
	public class ProductVM
	{
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
