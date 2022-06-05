using CalendarWebAppV2.Data;
using CalendarWebAppV2.Models.AppointmentsViewModels;
using CalendarWebAppV2.Models.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CalendarWebAppV2.Controllers
{
    public class AppointmentsController : Controller
    {
        private CalendarWebAppDbContext context;

        public AppointmentsController(CalendarWebAppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Would like to be able to direct to users profile like:
        // calendarwebapp.com/jonathanmantello
        // (Like linkedin.com/jonathanmantello) 
        public IActionResult Schedule(int hostId)
        {
            ScheduleVM model = new ScheduleVM();

            // Get host
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

            //// CASE: User is an appointment participant
            //foreach (int appointmentId in context.AppointmentParticipants
            //    .Where(ap => ap.ParticipantId == host.Id)
            //    .Select(ap => ap.AppointmentId)
            //    .ToList())
            //{ appointmentIds.Add(appointmentId); }

            var appointments = context.Appointments
                .Where(a => appointmentIds.Contains(a.Id))
                .ToList();

            model.Appointments = appointments;

            return View(model);
        }

        [HttpPost]
        public IActionResult Schedule(int hostId, DateTimeOffset dateTimeSelection, int duration)
        {
            // Declare model
            EnterDetailsVM model = new EnterDetailsVM();

            // Get host
            Hosts host = context.Hosts
                .SingleOrDefault(h => h.Id == hostId);

            if (host == null) return NotFound();

            model.Host = host;
            model.Duration = duration;
            model.DateTimeSelection = dateTimeSelection;
            model.EndTime = dateTimeSelection.AddMinutes(model.Duration);

            return View("EnterDetails", model);
        }

        [HttpPost]
        public IActionResult EnterDetails(EnterDetailsVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Get user
            Hosts user = context.Hosts
                .Include(u => u.HostAvailability)
                .SingleOrDefault(u => u.Id == model.HostId);

            if (user == null) return NotFound();

            // Get participant
            Participants participant = context.Participants.SingleOrDefault(u => u.Email == model.ParticipantEmail);

            if (participant == null)
            {
                //// Create new user
                //participant = new Users();

                //participant.FirstName = model.ParticipantFirstName;
                //participant.Email = model.ParticipantEmail;

                //if (!string.IsNullOrEmpty(model.ParticipantLastName))
                //{
                //    participant.LastName = model.ParticipantLastName;
                //}

                //if (!string.IsNullOrEmpty(model.ParticipantPhone))
                //{
                //    participant.Phone = model.ParticipantPhone;
                //}

                // Add new user to database
            }

            // Create new appointment
            Appointments appointment = new Appointments();
            
            DateTimeOffset appointmentStart = model.DateTimeSelection;
            DateTimeOffset appointmentEnd = model.DateTimeSelection.AddMinutes(model.Duration);

            appointment.Start = appointmentStart;
            appointment.End = appointmentEnd;

            if(!string.IsNullOrEmpty(model.Memo))
            {
                appointment.Memo = model.Memo;
            }

            // ?
            // Add user as host
            // Add participant as participant



            return Json(model);
        }

    }
}
