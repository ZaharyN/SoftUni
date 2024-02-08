using Microsoft.AspNetCore.Mvc;
using ShoppingListApp.Contracts;

namespace ShoppingListApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
			productService = _productService;
        }
        public IActionResult Index()
		{
			return View();
		}
	}
}
