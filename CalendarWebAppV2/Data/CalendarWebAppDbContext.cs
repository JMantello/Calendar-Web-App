using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CalendarWebAppV2.Models.EntityModels;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CalendarWebAppV2.Data
{
    public partial class CalendarWebAppDbContext : DbContext
    {
        public CalendarWebAppDbContext()
        {
        }

        public CalendarWebAppDbContext(DbContextOptions<CalendarWebAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppointmentHosts> AppointmentHosts { get; set; }
        public virtual DbSet<AppointmentParticipants> AppointmentParticipants { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<HostAvailability> HostAvailability { get; set; }
        public virtual DbSet<Hosts> Hosts { get; set; }
        public virtual DbSet<Participants> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentHosts>(entity =>
            {
                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentHosts)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentHosts_To_Appointments");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.AppointmentHosts)
                    .HasForeignKey(d => d.HostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentHost_To_Users");
            });

            modelBuilder.Entity<AppointmentParticipants>(entity =>
            {
                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentParticipants)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentParticipants_To_Appointments");

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.AppointmentParticipants)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentParticipants_To_Participants");
            });

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.Property(e => e.Memo).IsUnicode(false);
            });

            modelBuilder.Entity<HostAvailability>(entity =>
            {
                entity.HasOne(d => d.Host)
                    .WithMany(p => p.HostAvailability)
                    .HasForeignKey(d => d.HostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HostsAvailability_To_Hosts");
            });

            modelBuilder.Entity<Hosts>(entity =>
            {
                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.ProfileImage).IsUnicode(false);

                entity.Property(e => e.UniqueEndpoint).IsUnicode(false);
            });

            modelBuilder.Entity<Participants>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
