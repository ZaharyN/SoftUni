using GameZone.Data;
using GameZone.Data.DataModels;
using GameZone.Models.GameModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Claims;
using static GameZone.Data.Constants.DataConstants;

namespace GameZone.Controllers
{
	[Authorize]
	public class GameController : Controller
	{
		private readonly GameZoneDbContext context;

		public GameController(GameZoneDbContext _context)
		{
			context = _context;
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var form = new GameAddFormView()
			{
				Genres = await context.Genres
				.AsNoTracking()
				.Select(g => new GenreViewModel()
				{
					Id = g.Id,
					Name = g.Name
				})
				.ToListAsync()
			};

			return View(form);
		}

		[HttpPost]
		public async Task<IActionResult> Add(GameAddFormView form)
		{
			if (!DateTime.TryParseExact(
				form.ReleasedOn,
				GameReleasedOnDateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out DateTime releasedOn))
			{
				ModelState.AddModelError(form.ReleasedOn, $"Invalid date! Format must be: {GameReleasedOnDateFormat}");
			}

			if (!ModelState.IsValid)
			{
				form.Genres = await context.Genres
				.AsNoTracking()
				.Select(g => new GenreViewModel()
				{
					Id = g.Id,
					Name = g.Name
				})
				.ToListAsync();

				return View();
			}

			string userid = GetUserId();

			Game game = new Game()
			{
				Title = form.Title,
				Description = form.Description,
				ImageUrl = form.ImageUrl,
				PublisherId = userid,
				ReleasedOn = releasedOn,
				GenreId = form.GenreId
			};

			await context.Games.AddAsync(game);
			await context.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var models = await context.Games
				.AsNoTracking()
				.Select(g => new GameAllViewModel()
				{
					Id = g.Id,
					Title = g.Title,
					ImageUrl = g.ImageUrl,
					Publisher = g.Publisher.UserName,
					Genre = g.Genre.Name,
					ReleasedOn = g.ReleasedOn.ToString(GameReleasedOnDateFormat)
				})
				.ToListAsync();

			return View(models);
		}


		public async Task<IActionResult> AddToMyZone(int id)
		{
			Game? game = await context.Games
				.Include(g => g.GamersGames)
				.FirstOrDefaultAsync(g => g.Id == id);

			if (game == null)
			{
				return NotFound();
			}

			string userId = GetUserId();

			if (game.GamersGames.Any(gg => gg.GamerId == userId))
			{
				return RedirectToAction(nameof(All));
			}

			game.GamersGames.Add(new GamerGame()
			{
				GameId = game.Id,
				GamerId = userId,
			});

			await context.SaveChangesAsync();

			return RedirectToAction(nameof(MyZone));
		}

		[HttpGet]
		public async Task<IActionResult> MyZone()
		{
			string userId = GetUserId();

			var models = await context.GamersGames
				.Where(gg => gg.GamerId == userId)
				.AsNoTracking()
				.Select(gg => new GameAllViewModel()
				{
					Id = gg.Game.Id,
					Title = gg.Game.Title,
					ImageUrl = gg.Game.ImageUrl,
					Publisher = gg.Game.Publisher.UserName,
					Genre = gg.Game.Genre.Name,
					ReleasedOn = gg.Game.ReleasedOn.ToString(GameReleasedOnDateFormat)
				})
				.ToListAsync();
				
			return View(models);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var game = await context.Games.FindAsync(id);

			if(game == null)
			{
				return NotFound();
			}

			string userId = GetUserId();
			if(game.PublisherId != userId)
			{
				return Unauthorized();
			}

			var form = new GameEditFormView()
			{
				Title = game.Title,
				Description = game.Description,
				ImageUrl = game.ImageUrl,
				ReleasedOn = game.ReleasedOn.ToString(GameReleasedOnDateFormat),
				GenreId = game.GenreId,
				Genres = await context.Genres
					.AsNoTracking()
					.Select(g => new GenreViewModel()
					{
						Id = g.Id,
						Name = g.Name
					})
					.ToListAsync()
			};

			return View(form);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(GameEditFormView form)
		{
			if (!DateTime.TryParseExact(
				form.ReleasedOn,
				GameReleasedOnDateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out DateTime releasedOn))
			{
				ModelState.AddModelError(form.ReleasedOn, $"Invalid date! Format must be: {GameReleasedOnDateFormat}");
			}

			if (!ModelState.IsValid)
			{
				form.Genres = await context.Genres
				.AsNoTracking()
				.Select(g => new GenreViewModel()
				{
					Id = g.Id,
					Name = g.Name
				})
				.ToListAsync();

				return View();
			}

			var game = await context.Games.FindAsync(form.Id);

			if(game == null)
			{
				return NotFound();
			}

			string userId = GetUserId();
			if(game.PublisherId != userId)
			{
				return Unauthorized();
			}

			game.Title = form.Title;
			game.Description = form.Description;
			game.ImageUrl = form.ImageUrl;
			game.ReleasedOn = releasedOn;
			game.GenreId = form.GenreId;

			await context.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var model = await context.Games
				.Where(g => g.Id == id)
				.AsNoTracking()
				.Select(g => new GameDetailsViewModel()
				{
					Id = g.Id,
					Title = g.Title,
					Description = g.Description,
					ImageUrl = g.ImageUrl,
					Publisher = g.Publisher.UserName,
					Genre = g.Genre.Name,
					ReleasedOn = g.ReleasedOn.ToString(GameReleasedOnDateFormat)
				})
				.FirstOrDefaultAsync();

			if(model == null)
			{
				return BadRequest();
			}

			return View(model);
		}

		public async Task<IActionResult> StrikeOut(int id)
		{
			var game = await context.Games
				.Include(g => g.GamersGames)
				.FirstOrDefaultAsync(g => g.Id == id);

			if(game == null)
			{
				return BadRequest();
			}

			string userId = GetUserId();

			var gamerGame = game.GamersGames.FirstOrDefault(gg => gg.GamerId == userId);
			if(gamerGame == null)
			{
				return BadRequest();
			}

			if(game.GamersGames.Any(gg => gg.GamerId == userId))
			{
				game.GamersGames.Remove(gamerGame);
			}
			await context.SaveChangesAsync();

			return RedirectToAction(nameof(MyZone));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var game = await context.Games.FindAsync(id);

			if (game == null)
			{
				return NotFound();
			}
			string userId = GetUserId();

			if (game.PublisherId != userId)
			{
				return Unauthorized();
			}

			var form = await context.Games
				.Include(g => g.GamersGames)
				.Where(g => g.Id == id)
				.Select(g => new GameDeleteFormView()
				{
					Id = g.Id,
					Title = g.Title,
					Publisher = g.Publisher.UserName
				})
				.FirstOrDefaultAsync();

			return View(form);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(GameDeleteFormView form)
		{
			var game = await context.Games.FindAsync(form.Id);

			if(game == null)
			{
				return NotFound();
			}

			string userId = GetUserId();
			if(game.PublisherId != userId)
			{
				return Unauthorized();
			}

			var gamersGame = await context.GamersGames
				.Where(gg => gg.GameId == game.Id)
				.ToListAsync();

			context.GamersGames.RemoveRange(gamersGame);
			context.Games.Remove(game);

			await context.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
