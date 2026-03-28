using System.ComponentModel.DataAnnotations;
using EventBooking.API.Validators;

namespace EventBooking.API.DTOs
{
    // ─── EVENT DTOs ───────────────────────────────────────────────────────────

    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string Category { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int BookedSeats { get; set; }   // computed
    }

    public class CreateEventDto
    {
        [Required(ErrorMessage = "Event title is required.")]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [FutureDate(ErrorMessage = "Event date must be in the future.")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Location { get; set; } = string.Empty;

        [Range(1, 10000)]
        public int AvailableSeats { get; set; }

        [Range(0, 100000)]
        public decimal TicketPrice { get; set; }

        public string Category { get; set; } = "General";
        public string OrganizerName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class UpdateEventDto : CreateEventDto
    {
        public bool IsActive { get; set; } = true;
    }

    // ─── BOOKING DTOs ─────────────────────────────────────────────────────────

    public class BookingDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public string EventDate { get; set; } = string.Empty;
        public string EventLocation { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int SeatsBooked { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ConfirmationCode { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
    }

    public class CreateBookingDto
    {
        [Required(ErrorMessage = "Event ID is required.")]
        public int EventId { get; set; }

        [Range(1, 20, ErrorMessage = "You can book between 1 and 20 seats.")]
        public int SeatsBooked { get; set; }
    }

    // ─── AUTH DTOs ────────────────────────────────────────────────────────────

    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime Expiration { get; set; }
    }
}
