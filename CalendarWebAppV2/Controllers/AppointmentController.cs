﻿using CalendarWebAppV2.Data;
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
            CreateAppointmentVM model = new CreateAppointmentVM();

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
        public IActionResult Schedule(int userId, DateTime dateTimeSelection, int duration)
        {
            return Content($"{userId} - {dateTimeSelection} - {duration}");
        }

    }
}
