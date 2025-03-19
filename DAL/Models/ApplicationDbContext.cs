using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId);

            
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FullName = "John Doe", Email = "john@example.com", BirthDate = new DateTime(2000, 5, 15), NationalId = "12345678901234", PhoneNumber = "01234567890" },
                new Student { Id = 2, FullName = "Jane Smith", Email = "jane@example.com", BirthDate = new DateTime(1999, 8, 25), NationalId = "98765432109876", PhoneNumber = "01123456789" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Mathematics", Description = "Basic Math Course", MaxCapacity = 30 },
                new Course { Id = 2, Title = "Physics", Description = "Intro to Physics", MaxCapacity = 25 }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, StudentId = 1, CourseId = 1 },
                new Enrollment { Id = 2, StudentId = 2, CourseId = 2 }
            );
        }
    }
}
