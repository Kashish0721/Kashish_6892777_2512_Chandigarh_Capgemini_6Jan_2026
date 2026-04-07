using Microsoft.EntityFrameworkCore;
using LearningPlatformAPI.Models;

namespace LearningPlatformAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1-1
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId);

            // Many-to-Many
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.UserId, e.CourseId });
modelBuilder.Entity<Enrollment>()
    .HasOne(e => e.User)
    .WithMany(u => u.Enrollments)
    .HasForeignKey(e => e.UserId)
    .OnDelete(DeleteBehavior.NoAction);   

modelBuilder.Entity<Enrollment>()
    .HasOne(e => e.Course)
    .WithMany(c => c.Enrollments)
    .HasForeignKey(e => e.CourseId)
    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}