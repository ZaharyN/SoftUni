using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using static SeminarHub.Data.Constants.DataConstants;

namespace SeminarHub.Data.DataModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public List<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}