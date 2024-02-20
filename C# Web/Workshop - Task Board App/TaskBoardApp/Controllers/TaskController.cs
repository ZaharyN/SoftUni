using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
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
            if(!(await GetBoards()).Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId),"Board does not exist!");
            }
            string currentUserId = GetUserId();

            if(!ModelState.IsValid)
            {
                model.Boards = await GetBoards();
                return View(model);
            }


            
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
    }
}
