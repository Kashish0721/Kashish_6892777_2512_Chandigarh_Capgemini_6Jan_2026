using AutoMapper;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookingsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value
                     ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var id) ? id : 0;
        }

        /// <summary>GET api/bookings — Get bookings for the current user.</summary>
        [HttpGet]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = GetCurrentUserId();
            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();

            return Ok(_mapper.Map<List<BookingDto>>(bookings));
        }

        /// <summary>GET api/bookings/all — Get all bookings (Admin only).</summary>
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();

            return Ok(_mapper.Map<List<BookingDto>>(bookings));
        }

        /// <summary>GET api/bookings/{id} — Get a single booking.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound(new { message = "Booking not found." });

            if (!isAdmin && booking.UserId != userId)
                return Forbid();

            return Ok(_mapper.Map<BookingDto>(booking));
        }

        /// <summary>POST api/bookings — Book tickets for an event.</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == 0)
                return Unauthorized(new { message = "User identity could not be determined." });

            // Load event with its current bookings
            var ev = await _context.Events
                .Include(e => e.Bookings.Where(b => b.Status == "Confirmed"))
                .FirstOrDefaultAsync(e => e.Id == dto.EventId && e.IsActive);

            if (ev == null)
                return NotFound(new { message = "Event not found or no longer active." });

            if (ev.Date <= DateTime.UtcNow)
                return BadRequest(new { message = "Cannot book tickets for a past event." });

            var bookedSeats = ev.Bookings.Sum(b => b.SeatsBooked);
            var remainingSeats = ev.AvailableSeats - bookedSeats;

            if (dto.SeatsBooked > remainingSeats)
                return BadRequest(new
                {
                    message = $"Only {remainingSeats} seat(s) remaining for this event."
                });

            // Prevent duplicate booking
            var existingBooking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.EventId == dto.EventId
                                       && b.UserId == userId
                                       && b.Status == "Confirmed");

            if (existingBooking != null)
                return Conflict(new { message = "You already have an active booking for this event." });

            var booking = _mapper.Map<Booking>(dto);
            booking.UserId = userId;
            booking.TotalAmount = ev.TicketPrice * dto.SeatsBooked;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Reload with navigation properties for the response
            await _context.Entry(booking).Reference(b => b.Event).LoadAsync();
            await _context.Entry(booking).Reference(b => b.User).LoadAsync();

            return CreatedAtAction(nameof(GetById), new { id = booking.Id },
                _mapper.Map<BookingDto>(booking));
        }

        /// <summary>DELETE api/bookings/{id} — Cancel a booking.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound(new { message = "Booking not found." });

            if (!isAdmin && booking.UserId != userId)
                return Forbid();

            if (booking.Status == "Cancelled")
                return BadRequest(new { message = "Booking is already cancelled." });

            if (booking.Event != null && booking.Event.Date <= DateTime.UtcNow.AddHours(24))
                return BadRequest(new { message = "Cancellations must be made at least 24 hours before the event." });

            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking cancelled successfully.", bookingId = id });
        }
    }
}
