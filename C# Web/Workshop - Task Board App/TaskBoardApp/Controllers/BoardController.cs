﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly TaskBoardAppDbContext context;

        public BoardController(TaskBoardAppDbContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> Index()
        {
            List<BoardViewModel> boards = await context.Boards
                .Select(b => new BoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TaskViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName
                    })
                })
                .ToListAsync();

            return View(boards);
        }
    }
}
