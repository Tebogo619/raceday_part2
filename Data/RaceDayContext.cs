using Microsoft.EntityFrameworkCore;
using raceday_part2.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace raceday_part2.Data
{
    public class RaceDayContext
    {
        public RaceDayContext(DbContextOptions<RaceDayContext> options) : base(options) { }
        public DbSet<Organiser> Organisers => Set<Organiser>();
        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Enrolment> Enrolments => Set<Enrolment>();
        public DbSet<Result> Results => Set<Result>();
        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Organiser>().ToTable("Organiser").HasIndex(x => x.Email).IsUnique();
            b.Entity<Participant>().ToTable("Participant").HasIndex(x => x.Email).IsUnique();
            b.Entity<Event>().ToTable("Event");
            b.Entity<Category>().ToTable("Category");
            b.Entity<Enrolment>().ToTable("Enrolment").HasIndex(x => new { x.ParticipantID, x.CategoryID }).IsUnique();
            b.Entity<Result>().ToTable("Result").HasIndex(x => x.EnrolmentID).IsUnique();
            b.Entity<Organiser>().Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            b.Entity<Participant>().Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            b.Entity<Event>().Property(x => x.DistanceKm).HasColumnType("decimal(6,2)");
            b.Entity<Category>().Property(x => x.DistanceKm).HasColumnType("decimal(5,2)");
            b.Entity<Category>().Property(x => x.EntryFee).HasColumnType("decimal(8,2)").HasDefaultValue(0m);
            b.Entity<Category>().Property(x => x.MaxParticipants).HasDefaultValue(100);
            b.Entity<Event>().Property(x => x.EventType).HasDefaultValue("Run");
            b.Entity<Event>().Property(x => x.Status).HasDefaultValue("Draft");
            b.Entity<Enrolment>().Property(x => x.EnrolmentDate).HasDefaultValueSql("GETDATE()");
            b.Entity<Enrolment>().Property(x => x.Status).HasDefaultValue("Pending");
            b.Entity<Result>().Property(x => x.Status).HasDefaultValue("Pending");
            b.Entity<Event>().HasOne(x => x.Organiser).WithMany(x => x.Events).HasForeignKey(x => x.OrganiserID).OnDelete(DeleteBehavior.Restrict);
            b.Entity<Category>().HasOne(x => x.Event).WithMany(x => x.Categories).HasForeignKey(x => x.EventID).OnDelete(DeleteBehavior.Restrict);
            b.Entity<Enrolment>().HasOne(x => x.Participant).WithMany(x => x.Enrolments).HasForeignKey(x => x.ParticipantID).OnDelete(DeleteBehavior.Restrict);
            b.Entity<Enrolment>().HasOne(x => x.Category).WithMany(x => x.Enrolments).HasForeignKey(x => x.CategoryID).OnDelete(DeleteBehavior.Restrict);
            b.Entity<Result>().HasOne(x => x.Enrolment).WithOne(x => x.Result).HasForeignKey<Result>(x => x.EnrolmentID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
