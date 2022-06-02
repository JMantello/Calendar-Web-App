using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CalendarWebAppV2.Models
{
    public partial class Appointments
    {
        public Appointments()
        {
            AppointmentHosts = new HashSet<AppointmentHosts>();
            AppointmentParticipants = new HashSet<AppointmentParticipants>();
        }

        [Key]
        public int Id { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }

        [StringLength(300)]
        public string Memo { get; set; }

        [InverseProperty("Appointment")]
        public virtual ICollection<AppointmentHosts> AppointmentHosts { get; set; }

        [InverseProperty("Appointment")]
        public virtual ICollection<AppointmentParticipants> AppointmentParticipants { get; set; }
    }
}
