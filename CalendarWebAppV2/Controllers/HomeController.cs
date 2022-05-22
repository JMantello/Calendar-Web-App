using CalendarWebAppV2.Data;
using CalendarWebAppV2.Models;
using CalendarWebAppV2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarWebAppV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CalendarWebAppDbContext context;

        public HomeController(ILogger<HomeController> logger, CalendarWebAppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        // Would like to be able to direct to users profile like:
        // calendarwebapp.com/jonathanmantello
        // (Like linkedin.com/jonathanmantello) 
        public IActionResult Index()
        {
            return RedirectToAction("CreateAppointment");
        }

        public IActionResult CreateAppointment()
        {
            CreateAppointmentVM model = new CreateAppointmentVM();
            
            // Get user
            Users user = context.Users
                .Include(u => u.UsersAvailability)
                .SingleOrDefault(u => u.Id == 1);
            if (user == null) return NotFound();
            model.User = user;

            // Get user's appointments
            var appointmentIds = context.AppointmentHosts
                .Where(ah => ah.UserId == user.Id)
                .Select(ah => ah.AppointmentId)
                .ToList();
            
            var appointments = context.Appointments
                .Where(a => appointmentIds.Contains(a.Id))
                .ToList();

            model.Appointments = appointments;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
