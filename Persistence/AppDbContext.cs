using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityAttendee> ActivityAttendees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ActivityAttendee>().HasKey(a => new { a.UserId, a.ActivityId });
            builder.Entity<ActivityAttendee>()
                .HasOne(a => a.Activity)
                .WithMany(b => b.Attendees)
                .HasForeignKey(a => a.ActivityId);
            builder.Entity<ActivityAttendee>()
                .HasOne(a => a.User)
                .WithMany(b => b.Activities)
                .HasForeignKey(a => a.UserId);

        }
    }
}

