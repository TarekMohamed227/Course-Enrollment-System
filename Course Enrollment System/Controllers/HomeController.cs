using Microsoft.AspNetCore.Mvc;

namespace Course_Enrollment_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
