using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Models.ErrorMessages;
using static TaskBoardApp.Data.Constants.DataConstants;
using TaskBoardApp.Models.Board;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required(ErrorMessage = RequrieError)]
        [StringLength(
            TaskTitleMaxLength, 
            MinimumLength = TaskTitleMinLength,
            ErrorMessage = TaskTitleErrorMessage)]
            
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequrieError)]
        [StringLength(
            TaskDescriptionMaxLength, 
            MinimumLength = TaskDescriptionMinLength,
            ErrorMessage = TaskDescriptionErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequrieError)]
        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; } = new List<TaskBoardModel>();
    }
}
