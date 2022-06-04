using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CalendarWebAppV2.Models.EntityModels
{
    public partial class HostAvailability
    {
        [Key]
        public int Id { get; set; }
        public int HostId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [ForeignKey(nameof(HostId))]
        [InverseProperty(nameof(Hosts.HostAvailability))]
        public virtual Hosts Host { get; set; }
    }
}
