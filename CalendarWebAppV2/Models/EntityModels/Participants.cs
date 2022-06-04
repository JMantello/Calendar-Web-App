using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CalendarWebAppV2.Models.EntityModels
{
    public partial class Participants
    {
        public Participants()
        {
            AppointmentParticipants = new HashSet<AppointmentParticipants>();
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

        [InverseProperty("Participant")]
        public virtual ICollection<AppointmentParticipants> AppointmentParticipants { get; set; }
    }
}
