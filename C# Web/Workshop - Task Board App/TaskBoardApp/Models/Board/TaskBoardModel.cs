using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.Constants.DataConstants;

namespace TaskBoardApp.Models.Board
{
    public class TaskBoardModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
