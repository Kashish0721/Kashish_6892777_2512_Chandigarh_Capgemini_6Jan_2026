using System.ComponentModel.DataAnnotations;
using EventBooking.API.Validators;

namespace EventBooking.API.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event title is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event date is required.")]
        [FutureDate(ErrorMessage = "Event date must be in the future.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(500, ErrorMessage = "Location cannot exceed 500 characters.")]
        public string Location { get; set; } = string.Empty;

        [Range(1, 10000, ErrorMessage = "Available seats must be between 1 and 10,000.")]
        public int AvailableSeats { get; set; }

        // Additional business fields
        [Range(0, 100000, ErrorMessage = "Price must be a non-negative value.")]
        public decimal TicketPrice { get; set; }

        public string Category { get; set; } = "General"; // Music, Tech, Sports, etc.

        public string OrganizerName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
