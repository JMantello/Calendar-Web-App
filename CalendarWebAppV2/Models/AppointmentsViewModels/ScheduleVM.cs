using CalendarWebAppV2.Models.EntityModels;
using System.Collections.Generic;

namespace CalendarWebAppV2.Models.AppointmentsViewModels
{
    public class ScheduleVM
    {
        public Hosts Host { get; set; }
        public List<Appointments> Appointments { get; set; }
    }
}
