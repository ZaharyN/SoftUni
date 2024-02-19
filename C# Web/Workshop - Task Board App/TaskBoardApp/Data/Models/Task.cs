using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TaskBoardApp.Data.Constants.DataConstants;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(TaskDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; }
        public int? BoardId { get; set; }

        [ForeignKey(nameof(BoardId))]
        public Board? Board { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
    }
}

//•	Id – a unique integer, Primary Key
//•	Title – a string with min length 5 and max length 70 (required)
//•	Description – a string with min length 10 and max length 1000 (required)
//•	CreatedOn – date and time
//•	BoardId – an integer
//•	Board – a Board object
//•	OwnerId – an integer(required)
//•	Owner – an IdentityUser object