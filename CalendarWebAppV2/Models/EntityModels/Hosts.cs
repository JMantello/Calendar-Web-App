using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CalendarWebAppV2.Models.EntityModels
{
    public partial class Hosts
    {
        public Hosts()
        {
            AppointmentHosts = new HashSet<AppointmentHosts>();
            HostAvailability = new HashSet<HostAvailability>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Phone { get; set; }
        [StringLength(300)]
        public string Bio { get; set; }
        [StringLength(50)]
        public string UniqueEndpoint { get; set; }
        [StringLength(80)]
        public string ProfileImage { get; set; }

        [InverseProperty("Host")]
        public virtual ICollection<AppointmentHosts> AppointmentHosts { get; set; }
        [InverseProperty("Host")]
        public virtual ICollection<HostAvailability> HostAvailability { get; set; }
    }
}
