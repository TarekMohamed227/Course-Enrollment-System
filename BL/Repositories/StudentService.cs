using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Models;

namespace BL.Repositories
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAllStudents()
            => _context.Students.ToList();
        public Student? GetStudentById(int id)
            => _context.Students.Find(id);
        public void AddStudent(Student student)
        {
           
            if (_context.Students.Any(s =>
                s.FullName.ToLower() == student.FullName.ToLower() ||
                s.Email.ToLower() == student.Email.ToLower() ||
                s.NationalId == student.NationalId ||
                s.PhoneNumber == student.PhoneNumber))
            {
                throw new Exception("Student with the same Name, Email, National ID, or Phone Number already exists.");
            }

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void UpdateStudent(Student student)
        { _context.Students.Update(student);
            _context.SaveChanges();
        }
        public void DeleteStudent(int id)
        { var student = _context.Students.Find(id);
            if (student != null)
            { _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
