using BL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Course_Enrollment_System.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public EnrollmentController(IEnrollmentService enrollmentService, IStudentService studentService, ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        
        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            var enrollments = _enrollmentService.GetAllEnrollmentsWithDetails()
                                               .Skip((page - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToList();

            
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalEnrollments = _enrollmentService.GetAllEnrollmentsWithDetails().Count();

            return View(enrollments);
        }


        public IActionResult Create()
        {
            var students = _studentService.GetAllStudents();
            var courses = _courseService.GetAllCourses();

            
            ViewBag.Students = new SelectList(students, "Id", "FullName");
            ViewBag.Courses = new SelectList(courses, "Id", "Title");

            return View();
        }





        [HttpPost]
        public IActionResult Create(Enrollment enrollment)
        {
           
            if (_enrollmentService.IsAlreadyEnrolled(enrollment.StudentId, enrollment.CourseId))
            {
                ViewBag.ErrorMessage = "Student is already enrolled in this course.";
                ViewBag.Students = new SelectList(_studentService.GetAllStudents(), "Id", "FullName");
                ViewBag.Courses = new SelectList(_courseService.GetAllCourses(), "Id", "Title");
                return View(enrollment); 
            }

            
            var course = _courseService.GetCourseById(enrollment.CourseId);
            var enrolledCount = _enrollmentService.GetEnrollmentsByCourseId(course.Id).Count();

            if (enrolledCount >= course.MaxCapacity)
            {
                ViewBag.ErrorMessage = "Course is full. No available slots.";
                ViewBag.Students = new SelectList(_studentService.GetAllStudents(), "Id", "FullName");
                ViewBag.Courses = new SelectList(_courseService.GetAllCourses(), "Id", "Title");
                return View(enrollment);
            }

         
            _enrollmentService.AddEnrollment(enrollment);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Delete(int id)
        {
            var enrollment = _enrollmentService.GetEnrollmentById(id);
            if (enrollment == null)
                return NotFound();
            return View(enrollment);
        }

        
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _enrollmentService.UnenrollStudent(id);
            return RedirectToAction(nameof(Index));
        }

       
        [HttpGet]
        public IActionResult GetAvailableSlots(int courseId)
        {
            var course = _courseService.GetCourseById(courseId);
            var enrolledCount = _enrollmentService.GetEnrollmentsByCourseId(courseId).Count();
            var availableSlots = course?.MaxCapacity - enrolledCount;

            return Json(availableSlots);
        }
    }
}
