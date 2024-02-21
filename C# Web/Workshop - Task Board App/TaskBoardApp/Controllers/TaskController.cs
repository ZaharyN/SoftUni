using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Globalization;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Controllers
{
	[Authorize]
	public class TaskController : Controller
	{
		private readonly TaskBoardAppDbContext context;
		public TaskController(TaskBoardAppDbContext _data)
		{
			context = _data;
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			TaskFormModel model = new TaskFormModel()
			{
				Boards = await GetBoards()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(TaskFormModel model)
		{
			if (!(await GetBoards()).Any(b => b.Id == model.BoardId))
			{
				ModelState.AddModelError(nameof(model.BoardId), "Board does not exist!");
			}
			string currentUserId = GetUserId();

			if (!ModelState.IsValid)
			{
				model.Boards = await GetBoards();
				return View(model);
			}

			Data.Models.Task task = new Data.Models.Task()
			{
				Title = model.Title,
				Description = model.Description,
				CreatedOn = DateTime.Now,
				BoardId = model.BoardId,
				OwnerId = currentUserId
			};

			await context.Tasks.AddAsync(task);
			await context.SaveChangesAsync();

			var boards = context.Boards;

			return RedirectToAction("Index", "Board");
		}

		private string GetUserId()
		{
			return User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		private async Task<IEnumerable<TaskBoardModel>> GetBoards()
		{
			return await context.Boards
				.Select(b => new TaskBoardModel()
				{
					Id = b.Id,
					Name = b.Name
				})
				.ToListAsync();
		}

		public async Task<IActionResult> Details(int id)
		{
			var task = await context.Tasks
				.Where(t => t.Id == id)
				.Select(t => new TaskDetailsViewModel()
				{
					Id = t.Id,
					Title = t.Title,
					Description = t.Description,
					CreatedOn = t.CreatedOn.Value.ToString("dd/MM/yyyy HH:mm"),
					Board = t.Board.Name,
					Owner = t.Owner.UserName
				})
				.FirstOrDefaultAsync();

			if (task == null)
			{
				return BadRequest();
			}
			return View(task);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var task = await context.Tasks
				.FindAsync(id);

			if(task == null)
			{
				return BadRequest();
			}

			string currentUser = GetUserId();
			if(currentUser != task.OwnerId)
			{
				return Unauthorized();
			}

			TaskFormModel taskModel = new TaskFormModel
			{
				Title = task.Title,
				Description = task.Description,
				BoardId = task.BoardId,
				Boards = await GetBoards()
			};

			return View(taskModel);
		}

		[HttpPost]
		public async Task<IActionResult>Edit(TaskFormModel taskForm, int id)
		{
			var task = await context.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUser = GetUserId();
            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

			if(!(await GetBoards()).Any(b => b.Id == taskForm.BoardId))
			{
				ModelState.AddModelError(nameof(taskForm.BoardId), "Board does not exist.");
			}

			if(!ModelState.IsValid)
			{
                taskForm.Boards = await GetBoards();

				return View(taskForm);
			}

			task.Title = taskForm.Title;
			task.Description = taskForm.Description;
			task.BoardId = taskForm.BoardId;

			await context.SaveChangesAsync();
			return RedirectToAction("Index", "Board");
        }

		public async Task<IActionResult> Delete(int id)
		{
			var task = await context.Tasks.FindAsync(id);
			if(task == null)
			{
				return BadRequest();
			}

            string currentUser = GetUserId();
            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

			TaskViewModel model = new()
			{
				Id = task.Id,
				Title = task.Title,
				Description = task.Description
			};

			return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> Delete(TaskViewModel taskModel)
		{
			var task = await context.Tasks.FindAsync(taskModel.Id);
			if(task == null)
			{
				return BadRequest();
			}

            string currentUser = GetUserId();
            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

			context.Tasks.Remove(task);
			context.SaveChangesAsync();

			return RedirectToAction("Index", "Board");
        }
	}
}
