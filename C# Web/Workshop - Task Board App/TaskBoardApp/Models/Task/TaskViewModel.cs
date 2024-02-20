using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.Constants.DataConstants;

namespace TaskBoardApp.Models.Task
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TaskTitleMaxLength, MinimumLength = TaskTitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(TaskDescriptionMaxLength, MinimumLength = TaskDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Owner { get; set; } = string.Empty;
    }
}