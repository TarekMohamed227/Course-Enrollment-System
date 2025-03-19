using BL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Course_Enrollment_System.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            var courses = _courseService.GetAllCourses()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalCourses = _courseService.GetAllCourses().Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCourses / pageSize);
            ViewBag.CurrentPage = page;

            return View(courses);
        }


        public IActionResult Details(int id)
        {
            var course = _courseService.GetCourseById(id);
            if (course == null) 
            return NotFound();
            return View(course);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _courseService.AddCourse(course);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(course);
        }


        public IActionResult Edit(int id)
        {
            var course = _courseService.GetCourseById(id);
            if (course == null)
            return NotFound();
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.UpdateCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = _courseService.GetCourseById(id);
            if (course == null)
            return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.DeleteCourse(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
