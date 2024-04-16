using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Models.AdViewModels;
using System.Security.Claims;
using static SoftUniBazar.Data.Constants.DataConstants;

namespace SoftUniBazar.Controllers
{
	[Authorize]
	public class AdController : Controller
	{
		private readonly BazarDbContext context;

        public AdController(BazarDbContext _context)
        {
			context = _context;
		}

        [HttpGet]
		public async Task<IActionResult> All()
		{
			var models = await context.Ads
				.AsNoTracking()
				.Select(a => new AdAllViewModel()
				{
					Id = a.Id,
					Name = a.Name,
					ImageUrl = a.ImageUrl,
					CreatedOn = a.CreatedOn.ToString(AdDateTimeFormat),
					Category = a.Category.Name,
					Description = a.Description,
					Price = a.Price,
					Owner = a.Owner.UserName
				})
				.ToListAsync();

			return View(models);
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(int id)
		{
			var ad = await context.Ads
				.Include(a => a.AdsBuyers)
				.Where(a => a.Id == id)
				.FirstOrDefaultAsync();

			if(ad == null)
			{
				return NotFound();
			}

			string userId = GetUserId();

			if(ad.AdsBuyers.Any(ab => ab.BuyerId == userId))
			{
				return RedirectToAction(nameof(All));
			}

			ad.AdsBuyers.Add(new AdBuyer()
			{
				BuyerId = userId,
				AdId = ad.Id
			});

			await context.SaveChangesAsync();

			return RedirectToAction(nameof(Cart));
		}

		[HttpGet]
		public async Task<IActionResult> Cart()
		{
			string userId = GetUserId();

			var models = await context.AdsBuyers
				.Where(ab => ab.BuyerId == userId)
				.AsNoTracking()
				.Select(ab => new AdAllViewModel()
				{
					Id = ab.AdId,
					Name = ab.Ad.Name,
					ImageUrl = ab.Ad.ImageUrl,
					CreatedOn = ab.Ad.CreatedOn.ToString(AdDateTimeFormat),
					Category = ab.Ad.Category.Name,
					Description = ab.Ad.Description,
					Price = ab.Ad.Price,
					Owner = ab.Ad.Owner.UserName
				})
				.ToListAsync();

			return View(models);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var form = new AdAddFormViewModel()
			{
				Categories = await context.Categories
				.Select(c => new CategoryViewModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync()
			};

			return View(form);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AdAddFormViewModel form)
		{
			if (!ModelState.IsValid)
			{
				form.Categories = await context.Categories
				.Select(c => new CategoryViewModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();

				return View(form);
			}

			string userId = GetUserId();

			Ad ad = new()
			{
				Id = form.Id,
				Name = form.Name,
				Description = form.Description,
				Price = form.Price,
				OwnerId = userId,
				ImageUrl = form.ImageUrl,
				CreatedOn = DateTime.Now,
				CategoryId = form.CategoryId
			};

			await context.Ads.AddAsync(ad);
			await context.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var ad = await context.Ads.FindAsync(id);

			if(ad == null)
			{
				return NotFound();
			}

			string userId = GetUserId();

			if(ad.OwnerId != userId)
			{
				return Unauthorized();
			}

			var form = new AdEditFormViewModel()
			{
				Id = ad.Id,
				Name = ad.Name,
				Description = ad.Description,
				ImageUrl = ad.ImageUrl,
				Price = ad.Price,
				CategoryId = ad.CategoryId,
				Categories = await context.Categories
					.AsNoTracking()
					.Select(c => new CategoryViewModel()
					{
						Id = c.Id,
						Name = c.Name
					})
					.ToListAsync()
			};

			return View(form);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(AdEditFormViewModel form)
		{
			if (!ModelState.IsValid)
			{
				form.Categories = await context.Categories
					.AsNoTracking()
					.Select(c => new CategoryViewModel()
					{
						Id = c.Id,
						Name = c.Name
					})
					.ToListAsync();

				return View();
			}

			var ad = await context.Ads.FindAsync(form.Id);

			if(ad == null)
			{
				return NotFound();
			}

			string userId = GetUserId();

			if(ad.OwnerId != userId)
			{
				return Unauthorized();
			}

			ad.Name = form.Name;
			ad.Description = form.Description;
			ad.ImageUrl = form.ImageUrl;
			ad.Price = form.Price;
			ad.CategoryId = form.CategoryId;

			await context.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromCart(int id)
		{
			var ad = await context.Ads
				.Include(a => a.AdsBuyers)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (ad == null)
			{
				return NotFound();
			}

			string userId = GetUserId();

			var adBuyer = ad.AdsBuyers
				.FirstOrDefault(ab => ab.BuyerId == userId);

			if(adBuyer != null)
			{
				ad.AdsBuyers.Remove(adBuyer);
			}

			await context.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
