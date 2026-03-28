using AutoMapper;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EventsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>GET api/events — List all active events.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? category,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Events
                .Include(e => e.Bookings)
                .Where(e => e.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(e => e.Category == category);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(e =>
                    e.Title.Contains(search) ||
                    e.Description.Contains(search) ||
                    e.Location.Contains(search));

            var total = await query.CountAsync();
            var events = await query
                .OrderBy(e => e.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                total,
                page,
                pageSize,
                data = _mapper.Map<List<EventDto>>(events)
            });
        }

        /// <summary>GET api/events/{id} — Get a single event.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _context.Events
                .Include(e => e.Bookings)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

            if (ev == null) return NotFound(new { message = "Event not found." });

            return Ok(_mapper.Map<EventDto>(ev));
        }

        /// <summary>POST api/events — Create a new event (Admin only).</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ev = _mapper.Map<Event>(dto);
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ev.Id }, _mapper.Map<EventDto>(ev));
        }

        /// <summary>PUT api/events/{id} — Update an event (Admin only).</summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound(new { message = "Event not found." });

            _mapper.Map(dto, ev);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<EventDto>(ev));
        }

        /// <summary>DELETE api/events/{id} — Soft-delete an event (Admin only).</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound(new { message = "Event not found." });

            ev.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>GET api/events/categories — List available categories.</summary>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Events
                .Where(e => e.IsActive)
                .Select(e => e.Category)
                .Distinct()
                .ToListAsync();

            return Ok(categories);
        }
    }
}
