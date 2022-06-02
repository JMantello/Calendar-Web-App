using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarWebAppV2.Models.ViewModels
{
    public class EnterDetailsVM
    {
        public int UserId { get; set; }
        public Users User { get; set; }
        public DateTimeOffset DateTimeSelection { get; set; }
        public int Duration { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Your first name is required")]
        public string ParticipantFirstName { get; set; }

        [StringLength(50)]
        public string ParticipantLastName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Your email is required")]
        public string ParticipantEmail { get; set; }
        
        [StringLength(30)]
        public string ParticipantPhone { get; set; }

        [StringLength(300)]
        public string Memo { get; set; }

    }
}
