using CalendarWebAppV2.Data;
using CalendarWebAppV2.Models.EntityModels;
using CalendarWebAppV2.Models.HostsViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CalendarWebAppV2.Controllers
{
    public class HostsController : Controller
    {
        private CalendarWebAppDbContext context;

        public HostsController(CalendarWebAppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Hosts);
        }

        public IActionResult Appointments(int hostId)
        {
            // Create model
            AppointmentsVM model = new AppointmentsVM();

            // Get user
            Hosts host = context.Hosts
                .Include(h => h.HostAvailability)
                .SingleOrDefault(h => h.Id == hostId);

            if (host == null) return NotFound();

            model.Host = host;

            // Get user's appointments 
            // CASE: User is an appointment host
            var appointmentIds = context.AppointmentHosts
                .Where(ah => ah.HostId == host.Id)
                .Select(ah => ah.AppointmentId)
                .ToList();

            // CASE: User is an appointment participant
            foreach (int appointmentId in context.AppointmentParticipants
                .Where(ap => ap.ParticipantId == host.Id)
                .Select(ap => ap.AppointmentId)
                .ToList())
            { 
                appointmentIds.Add(appointmentId); 
            }

            var appointments = context.Appointments
                .Where(a => appointmentIds.Contains(a.Id))
                .ToList();

            model.Appointments = appointments;

            return View(model);
        }
    }
}
