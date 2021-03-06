using CalendarWebAppV2.Data;
using CalendarWebAppV2.Models;
using CalendarWebAppV2.Models.HomeViewModels;
using CalendarWebAppV2.ViewModels;
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
        
        public IActionResult Index()
        {
            //return RedirectToAction("Schedule", "Appointments", new { hostId = 1 });

            IndexVM model = new IndexVM();
            model.Hosts = context.Hosts.ToList();
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
