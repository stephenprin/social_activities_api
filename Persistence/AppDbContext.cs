using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; set; }
    // public DbSet<ActivityAttendee> ActivityAttendees { get; set; }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));

    //     builder.Entity<ActivityAttendee>()
    //         .HasOne(u => u.AppUser)
    //         .WithMany(a => a.Activities)
    //         .HasForeignKey(aa => aa.AppUserId);

    //     builder.Entity<ActivityAttendee>()
    //         .HasOne(a => a.Activity)
    //         .WithMany(u => u.Attendees)
    //         .HasForeignKey(aa => aa.ActivityId);
    // }
}
