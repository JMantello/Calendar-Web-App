using CalendarWebAppV2.Data;
using CalendarWebAppV2.Models;
using CalendarWebAppV2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CalendarWebAppV2.Controllers
{
    public class UsersController : Controller
    {
        private CalendarWebAppDbContext context;

        public UsersController(CalendarWebAppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Users);
        }

        public IActionResult Appointments(int userId)
        {
            // Create model
            AppointmentsVM model = new AppointmentsVM();

            // Get user
            Users user = context.Users
                .Include(u => u.UsersAvailability)
                .SingleOrDefault(u => u.Id == userId);

            if (user == null) return NotFound();

            model.User = user;

            // Get user's appointments 
            // CASE: User is an appointment host
            var appointmentIds = context.AppointmentHosts
                .Where(ah => ah.UserId == user.Id)
                .Select(ah => ah.AppointmentId)
                .ToList();

            // CASE: User is an appointment participant
            foreach (int appointmentId in context.AppointmentParticipants
                .Where(ap => ap.UserId == user.Id)
                .Select(ap => ap.AppointmentId)
                .ToList())
            { appointmentIds.Add(appointmentId); }

            var appointments = context.Appointments
                .Where(a => appointmentIds.Contains(a.Id))
                .ToList();

            model.Appointments = appointments;

            return View(model);
        }
    }
}
