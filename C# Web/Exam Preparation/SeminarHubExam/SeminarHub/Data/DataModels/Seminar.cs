using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using static SeminarHub.Data.Constants.DataConstants;

namespace SeminarHub.Data.DataModels
{
    public class Seminar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(SeminarTopicMaxLength)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(SeminarLecturerMaxLength)]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [MaxLength(SeminarDetailsMiaxLength)]
        public string Details { get; set; } = string.Empty;

        [Required]
        public string OrganizerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        public DateTime DateAndTime { get; set; }

        [Range(30,180)]
        public int Duration { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Category))]
        public int Categoryid { get; set; }

        public List<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }
}
