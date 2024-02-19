using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.Constants.DataConstants;

namespace TaskBoardApp.Models
{
    public class BoardViewModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(BoardNameMaxLength, MinimumLength = BoardNameMinLength)]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
