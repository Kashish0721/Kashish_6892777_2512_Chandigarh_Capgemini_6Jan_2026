using HealthcareSystem.API.Data;
using HealthcareSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.API.Controllers;

/// <summary>Advanced reporting endpoint (Admin only)</summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context) => _context = context;

    /// <summary>GET appointments report with date range filter</summary>
    [HttpGet("appointments")]
    public async Task<IActionResult> AppointmentsReport(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _context.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .AsQueryable();

        if (from.HasValue) query = query.Where(a => a.AppointmentDate >= from.Value);
        if (to.HasValue) query = query.Where(a => a.AppointmentDate <= to.Value);
        if (!string.IsNullOrEmpty(status)) query = query.Where(a => a.Status == status);

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(a => a.AppointmentDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new
            {
                a.Id,
                PatientName = a.Patient.User.FullName,
                DoctorName = a.Doctor.User.FullName,
                a.AppointmentDate,
                a.Status,
                a.Fee,
                a.CreatedAt
            })
            .ToListAsync();

        return Ok(new
        {
            items,
            totalCount = total,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling((double)total / pageSize)
        });
    }

    /// <summary>GET doctor performance stats</summary>
    [HttpGet("doctor-stats")]
    public async Task<IActionResult> DoctorStats()
    {
        var stats = await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Appointments)
            .Select(d => new
            {
                DoctorId = d.Id,
                DoctorName = d.User.FullName,
                TotalAppointments = d.Appointments.Count,
                CompletedAppointments = d.Appointments.Count(a => a.Status == "Completed"),
                PendingAppointments = d.Appointments.Count(a => a.Status == "Pending"),
                TotalRevenue = d.Appointments
                    .Where(a => a.Status == "Completed" && a.Fee.HasValue)
                    .Sum(a => a.Fee ?? 0)
            })
            .OrderByDescending(d => d.TotalAppointments)
            .ToListAsync();

        return Ok(stats);
    }

    /// <summary>GET monthly appointment trend</summary>
    [HttpGet("monthly-trend")]
    public async Task<IActionResult> MonthlyTrend([FromQuery] int year = 0)
    {
        if (year == 0) year = DateTime.UtcNow.Year;

        var trend = await _context.Appointments
            .Where(a => a.AppointmentDate.Year == year)
            .GroupBy(a => a.AppointmentDate.Month)
            .Select(g => new
            {
                Month = g.Key,
                Total = g.Count(),
                Completed = g.Count(a => a.Status == "Completed"),
                Cancelled = g.Count(a => a.Status == "Cancelled"),
                Revenue = g.Where(a => a.Status == "Completed" && a.Fee.HasValue).Sum(a => a.Fee ?? 0)
            })
            .OrderBy(g => g.Month)
            .ToListAsync();

        return Ok(trend);
    }
}
