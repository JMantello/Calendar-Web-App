using System.Collections.Generic;

namespace CalendarWebAppV2.Models.ViewModels
{
    public class AppointmentsVM
    {
        public Users User { get; set; }
        public List<Appointments> Appointments { get; set; }
    }
}
