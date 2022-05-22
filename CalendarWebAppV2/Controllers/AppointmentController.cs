using Microsoft.AspNetCore.Mvc;

namespace CalendarWebAppV2.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
