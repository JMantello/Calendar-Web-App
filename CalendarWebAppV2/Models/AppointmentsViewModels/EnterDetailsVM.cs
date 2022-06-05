using CalendarWebAppV2.Models.EntityModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarWebAppV2.Models.AppointmentsViewModels
{
    public class EnterDetailsVM
    {
        [Required]
        public int HostId { get; set; }

        public Hosts Host { get; set; }

        [Required]
        public DateTimeOffset DateTimeSelection { get; set; }
        
        public DateTimeOffset EndTime { get; set; }

        [Required]
        public int Duration { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "First name is required")]
        public string ParticipantFirstName { get; set; }

        [StringLength(50)]
        public string ParticipantLastName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Email address is required")]
        public string ParticipantEmail { get; set; }
        
        [StringLength(30)]
        public string ParticipantPhone { get; set; }

        [StringLength(300)]
        public string Memo { get; set; }

    }
}
