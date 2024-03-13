using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Constants.DataConstants;

namespace SeminarHub.Models
{
    public class SeminarAddViewModel
    {
        [Required]
        [StringLength(SeminarTopicMaxLength, 
            MinimumLength = SeminarTopicMinLength)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [StringLength(SeminarLecturerMaxLength,
            MinimumLength = SeminarLecturerMinLength)]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [StringLength(SeminarDetailsMiaxLength,
            MinimumLength = SeminarDetailsMinLength)]
        public string Details { get; set; } = string.Empty;

        public string OrganizerId { get; set; } = string.Empty;

        [Required]
        public string DateAndTime { get; set; } = string.Empty;

        [Required]
        [Range(SeminarDurationMinLength,SeminarDurationMaxLength)]
        public int? Duration { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
