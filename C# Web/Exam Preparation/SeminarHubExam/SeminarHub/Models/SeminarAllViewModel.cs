using Microsoft.AspNetCore.Identity;
using SeminarHub.Data.DataModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Constants.DataConstants;

namespace SeminarHub.Models
{
    public class SeminarAllViewModel
    {
        public int Id { get; set; }

        public string Topic { get; set; } = string.Empty;

        public string Lecturer { get; set; } = string.Empty;

        public string DateAndTime { get; set; } = string.Empty;

        public string Organizer { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
    }
}
