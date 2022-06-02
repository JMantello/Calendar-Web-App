using CalendarWebAppV2.Data;
using CalendarWebAppV2.Models;
using CalendarWebAppV2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CalendarWebAppV2.Controllers
{
    public class AppointmentController : Controller
    {
        private CalendarWebAppDbContext context;

        public AppointmentController(CalendarWebAppDbContext context)
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
        public IActionResult Schedule(int userId)
        {
            ScheduleVM model = new ScheduleVM();

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

        [HttpPost]
        public IActionResult Schedule(int userId, DateTimeOffset dateTimeSelection, int duration)
        {
            // Declare model
            EnterDetailsVM model = new EnterDetailsVM();

            // Get user
            Users user = context.Users
                .Include(u => u.UsersAvailability)
                .SingleOrDefault(u => u.Id == userId);

            if (user == null) return NotFound();

            model.User = user;
            model.DateTimeSelection = dateTimeSelection;
            model.Duration = duration;

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
            Users user = context.Users
                .Include(u => u.UsersAvailability)
                .SingleOrDefault(u => u.Id == model.UserId);

            if (user == null) return NotFound();

            // Get participant
            Users participant = context.Users.SingleOrDefault(u => u.Email == model.ParticipantEmail);

            if (participant == null)
            {
                // Create new user
                participant = new Users();

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
