using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CalendarWebAppV2.Models;

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
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersAvailability> UsersAvailability { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=JMANTELLO-DESKT\\SQLEXPRESS;Initial Catalog=CalendarWebAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentHosts>(entity =>
            {
                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentHosts)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentHosts_To_Appointments");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AppointmentHosts)
                    .HasForeignKey(d => d.UserId)
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

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AppointmentParticipants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppointmentParticipants_To_Users");
            });

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.Property(e => e.Memo).IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);
            });

            modelBuilder.Entity<UsersAvailability>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersAvailability)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UsersAvailability_To_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
