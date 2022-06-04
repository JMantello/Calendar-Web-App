using CalendarWebAppV2.Models.EntityModels;
using System.Collections.Generic;

namespace CalendarWebAppV2.Models.HostsViewModels
{
    public class AppointmentsVM
    {
        public Hosts Host { get; set; }
        public List<Appointments> Appointments { get; set; }
    }
}
