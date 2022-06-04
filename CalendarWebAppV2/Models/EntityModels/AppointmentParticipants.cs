using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CalendarWebAppV2.Models.EntityModels
{
    public partial class AppointmentParticipants
    {
        [Key]
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int AppointmentId { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty(nameof(Appointments.AppointmentParticipants))]
        public virtual Appointments Appointment { get; set; }
        [ForeignKey(nameof(ParticipantId))]
        [InverseProperty(nameof(Participants.AppointmentParticipants))]
        public virtual Participants Participant { get; set; }
    }
}
