using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CalendarWebAppV2.Models
{
    public partial class Users
    {
        public Users()
        {
            AppointmentHosts = new HashSet<AppointmentHosts>();
            AppointmentParticipants = new HashSet<AppointmentParticipants>();
            UsersAvailability = new HashSet<UsersAvailability>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Phone { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<AppointmentHosts> AppointmentHosts { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AppointmentParticipants> AppointmentParticipants { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UsersAvailability> UsersAvailability { get; set; }
    }
}
