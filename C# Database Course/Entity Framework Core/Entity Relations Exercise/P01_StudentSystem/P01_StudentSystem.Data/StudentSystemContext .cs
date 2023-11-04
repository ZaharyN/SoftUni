using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext 
        : DbContext
    {
        public StudentSystemContext(DbContextOptions dbContextOptions) 
            : base(dbContextOptions)
        {
        
        }
        
        private const string connection = "Server=ZAHARY;Database=StudentSystem;Integrated Security=True";


        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<Homework> Homeworks { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<StudentCourse> StudentsCourses { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connection);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new
                {
                    sc.StudentId,
                    sc.CourseId
                });
        }
    }
}
