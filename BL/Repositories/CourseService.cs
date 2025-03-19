using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Models;

namespace BL.Repositories
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllCourses()
            => _context.Courses.ToList();
        public Course? GetCourseById(int id)
            => _context.Courses.Find(id);
        public void AddCourse(Course course)
        {
          
            if (_context.Courses.Any(c => c.Title.ToLower() == course.Title.ToLower()))
            {
                throw new Exception("Course title already exists.");
            }

            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        { 
            _context.Courses.Update(course);
            _context.SaveChanges();
        }
        public void DeleteCourse(int id)
        { var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }
    }
}
