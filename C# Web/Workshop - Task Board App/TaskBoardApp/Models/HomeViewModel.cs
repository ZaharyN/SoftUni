﻿using System.Reflection.Metadata.Ecma335;

namespace TaskBoardApp.Models
{
    public class HomeViewModel
    {
        public int AllTasksCount { get; init; }
        public List<HomeBoardModel> BoardsWithTasksCount { get; set; } = new List<HomeBoardModel>();
        public int UserTasksCount { get; set; }
    }
}
