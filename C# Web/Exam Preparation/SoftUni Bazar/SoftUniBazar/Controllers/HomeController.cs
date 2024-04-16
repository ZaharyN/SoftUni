using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoftUniBazar.Controllers
{
	[AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			if (User != null
				&& User.Identity != null
				&& User.Identity.IsAuthenticated)
			{
				return RedirectToAction("All", "Ad");
			}

			return View();
        }
    }
}