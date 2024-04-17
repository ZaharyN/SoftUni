using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			if (User != null
				&& User.Identity != null
				&& User.Identity.IsAuthenticated)
			{
				return RedirectToAction("All", "Game");
			}

			return View();
        }

    }
}
