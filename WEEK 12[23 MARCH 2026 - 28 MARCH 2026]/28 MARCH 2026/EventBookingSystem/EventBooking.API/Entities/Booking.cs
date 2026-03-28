using System.ComponentModel.DataAnnotations;

namespace EventBooking.API.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Range(1, 20, ErrorMessage = "You can book between 1 and 20 seats per booking.")]
        public int SeatsBooked { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled, Pending

        public decimal TotalAmount { get; set; }

        public string ConfirmationCode { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

        // Navigation
        public Event? Event { get; set; }
        public User? User { get; set; }
    }
}
