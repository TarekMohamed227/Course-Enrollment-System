using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
            => _context.Enrollments.ToList();
        public Enrollment? GetEnrollmentById(int id)
            => _context.Enrollments.Find(id);

        public bool EnrollStudent(int studentId, int courseId)
        {
            var course = _context.Courses.Find(courseId);
            if (course == null || _context.Enrollments.Count(e => e.CourseId == courseId)
                >= course.MaxCapacity)
                return false;
            if (_context.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
                return false;
            _context.Enrollments.Add(new Enrollment { StudentId = studentId, CourseId = courseId });
            _context.SaveChanges();
            return true;
        }

        public void UnenrollStudent(int enrollmentId)
        {
            var enrollment = _context.Enrollments.Find(enrollmentId);
            if (enrollment != null)
            { _context.Enrollments.Remove(enrollment);
                _context.SaveChanges();
            }
        }
        public bool IsAlreadyEnrolled(int studentId, int courseId)
        {
            return _context.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public IEnumerable<Enrollment> GetEnrollmentsByCourseId(int courseId)
        {
            return _context.Enrollments.Where(e => e.CourseId == courseId).ToList();
        }
        public void AddEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }
        public IEnumerable<Enrollment> GetAllEnrollmentsWithDetails()
        {
            return _context.Enrollments
                           .Include(e => e.Student)
                           .Include(e => e.Course)
                           .ToList();
        }

    }
}
