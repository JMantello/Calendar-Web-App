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
            // CASE: User is host
            var appointmentIds = context.AppointmentHosts
                .Where(h => h.HostId == host.Id)
                .Select(h => h.AppointmentId)
                .ToList();

            // CASE: User is participant (Host is a participant in another appointment)
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
            Hosts host = context.Hosts
                .Include(u => u.HostAvailability)
                .SingleOrDefault(u => u.Id == model.HostId);

            if (host == null) return NotFound();

            model.Host = host;

            // Would like to get participant if existing user

            // Create participant
            Participants participant = new Participants();

            participant.FirstName = model.ParticipantFirstName;
            participant.Email = model.ParticipantEmail;

            if (!string.IsNullOrEmpty(model.ParticipantLastName))
            {
                participant.LastName = model.ParticipantLastName;
            }

            if (!string.IsNullOrEmpty(model.ParticipantPhone))
            {
                participant.Phone = model.ParticipantPhone;
            }

            // Create appointment
            Appointments appointment = new Appointments();

            DateTimeOffset appointmentStart = model.DateTimeSelection;
            DateTimeOffset appointmentEnd = model.DateTimeSelection.AddMinutes(model.Duration);

            appointment.Start = appointmentStart;
            appointment.End = appointmentEnd;

            if (!string.IsNullOrEmpty(model.Memo))
            {
                appointment.Memo = model.Memo;
            }

            // Create AppointmentHost
            AppointmentHosts ah = new AppointmentHosts();
            ah.AppointmentId = appointment.Id;
            ah.HostId = host.Id;

            // Create AppointmentParticipant
            AppointmentParticipants ap = new AppointmentParticipants();
            ap.AppointmentId = appointment.Id;
            ap.ParticipantId = participant.Id;

            // Add items to appointment
            appointment.AppointmentHosts.Add(ah);
            appointment.AppointmentParticipants.Add(ap);

            // Add participant
            context.Participants.Add(participant);
            
            // Add appointment to Db
            context.Appointments.Add(appointment);

            // Save Db
            context.SaveChanges();

            return View("Confirmed", model);
        }

    }
}
