using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(80)]
        [Unicode]
        public string Name { get; set; } = null!;

        [Unicode]
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Resource> Resources { get; set; } = null!;
        public virtual ICollection<StudentCourse> StudentsCourses { get; set; } = null!;
        public virtual ICollection<Homework> Homeworks { get; set; } = null!;

    }
}