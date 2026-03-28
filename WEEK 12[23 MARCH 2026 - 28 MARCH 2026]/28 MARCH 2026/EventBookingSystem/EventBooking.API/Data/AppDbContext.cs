using EventBooking.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Event configuration ──────────────────────────────────────
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.Location).IsRequired().HasMaxLength(500);
                entity.Property(e => e.TicketPrice).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.HasMany(e => e.Bookings)
                      .WithOne(b => b.Event)
                      .HasForeignKey(b => b.EventId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Booking configuration ────────────────────────────────────
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Status).HasMaxLength(50);
                entity.Property(b => b.ConfirmationCode).HasMaxLength(20);
                entity.Property(b => b.TotalAmount).HasColumnType("decimal(10,2)");
                entity.HasOne(b => b.User)
                      .WithMany(u => u.Bookings)
                      .HasForeignKey(b => b.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── User configuration ───────────────────────────────────────
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Role).HasMaxLength(50);
            });

            // ── Seed Data ────────────────────────────────────────────────
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed admin user (password: Admin@123)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Admin User",
                    Email = "admin@eventbooking.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "Admin",
                    PhoneNumber = "9876543210",
                    RegisteredAt = new DateTime(2024, 1, 1),
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    FullName = "John Doe",
                    Email = "john@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User@123"),
                    Role = "User",
                    PhoneNumber = "9123456789",
                    RegisteredAt = new DateTime(2024, 1, 15),
                    IsActive = true
                }
            );

            // Seed events
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Tech Summit 2025",
                    Description = "A premier technology conference featuring talks on AI, Cloud, and Web Development by industry leaders.",
                    Date = new DateTime(2025, 8, 15, 9, 0, 0),
                    Location = "Bangalore International Convention Centre, Bangalore",
                    AvailableSeats = 500,
                    TicketPrice = 2999,
                    Category = "Technology",
                    OrganizerName = "TechIndia Org",
                    ImageUrl = "https://images.unsplash.com/photo-1540575467063-178a50c2df87?w=800",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Event
                {
                    Id = 2,
                    Title = "AR Rahman Live Concert",
                    Description = "An unforgettable evening with the Oscar-winning maestro AR Rahman performing his greatest hits.",
                    Date = new DateTime(2025, 9, 20, 18, 30, 0),
                    Location = "DY Patil Stadium, Mumbai",
                    AvailableSeats = 5000,
                    TicketPrice = 1500,
                    Category = "Music",
                    OrganizerName = "ShowTime Events",
                    ImageUrl = "https://images.unsplash.com/photo-1501386761578-eaa54b5a8b5a?w=800",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Event
                {
                    Id = 3,
                    Title = "Startup Founders Meetup",
                    Description = "Network with 500+ founders, investors, and mentors. Pitch your idea and get funding opportunities.",
                    Date = new DateTime(2025, 7, 10, 10, 0, 0),
                    Location = "91springboard, Gurugram",
                    AvailableSeats = 200,
                    TicketPrice = 499,
                    Category = "Business",
                    OrganizerName = "StartupIndia Hub",
                    ImageUrl = "https://images.unsplash.com/photo-1515187029135-18ee286d815b?w=800",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Event
                {
                    Id = 4,
                    Title = "IPL Watch Party - Finals",
                    Description = "Watch the IPL Finals on a giant screen with fellow cricket fans. Food, drinks and prizes included!",
                    Date = new DateTime(2025, 6, 1, 19, 0, 0),
                    Location = "Hard Rock Cafe, Delhi",
                    AvailableSeats = 150,
                    TicketPrice = 799,
                    Category = "Sports",
                    OrganizerName = "FanZone India",
                    ImageUrl = "https://images.unsplash.com/photo-1540747913346-19e32dc3e97e?w=800",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                }
            );
        }
    }
}
