using Application.Interfaces.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Связь один ко многим между UserProfile и Courses
            modelBuilder.Entity<Courses>()
                .HasOne(c => c.UserProfile)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь один ко многим между Courses и Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Courses)
                .WithMany()
                .HasForeignKey(b => b.CoursesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}