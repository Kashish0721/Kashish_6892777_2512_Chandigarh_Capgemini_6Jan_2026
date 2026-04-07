using System.Security.Claims;
using HealthcareSystem.API.Services.Interfaces;
using HealthcareSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _service;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(IAppointmentService service, IPatientService patientService,
        IDoctorService doctorService, ILogger<AppointmentsController> logger)
    { _service = service; _patientService = patientService; _doctorService = doctorService; _logger = logger; }

    /// <summary>GET all appointments with pagination (Admin/Doctor)</summary>
    [HttpGet]
    [Authorize(Roles = "Admin,Doctor")]
    [ProducesResponseType(typeof(PagedResult<AppointmentDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
        => Ok(await _service.GetAllAsync(parameters));

    /// <summary>GET appointment by ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AppointmentDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound(new { message = $"Appointment {id} not found." });
        return Ok(result);
    }

    /// <summary>GET appointments by date — /api/appointments?date=2026-04-01</summary>
    [HttpGet("by-date")]
    [Authorize(Roles = "Admin,Doctor")]
    [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), 200)]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        => Ok(await _service.GetByDateAsync(date));

    /// <summary>GET my appointments (Patient)</summary>
    [HttpGet("my")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetMyAppointments()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var patient = await _patientService.GetByUserIdAsync(userId);
        if (patient == null) return NotFound(new { message = "Patient profile not found." });
        return Ok(await _service.GetByPatientAsync(patient.Id));
    }

    /// <summary>GET doctor's appointments</summary>
    [HttpGet("doctor/{doctorId:int}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetByDoctor([FromRoute] int doctorId)
        => Ok(await _service.GetByDoctorAsync(doctorId));

    /// <summary>POST book appointment (Patient)</summary>
    [HttpPost]
    [Authorize(Roles = "Patient")]
    [ProducesResponseType(typeof(AppointmentDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var patient = await _patientService.GetByUserIdAsync(userId);
        if (patient == null) return BadRequest(new { message = "Complete your patient profile first." });

        var result = await _service.CreateAsync(patient.Id, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>PUT full update of appointment (Doctor/Admin)</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Doctor,Admin")]
    [ProducesResponseType(typeof(AppointmentDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAppointmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdateAsync(id, dto);
        if (result == null) return NotFound(new { message = $"Appointment {id} not found." });
        return Ok(result);
    }

    /// <summary>PATCH partial update — status or notes only</summary>
    [HttpPatch("{id:int}")]
    [Authorize(Roles = "Doctor,Admin")]
    [ProducesResponseType(typeof(AppointmentDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] PatchAppointmentDto dto)
    {
        var result = await _service.PatchAsync(id, dto);
        if (result == null) return NotFound(new { message = $"Appointment {id} not found." });
        return Ok(result);
    }

    /// <summary>DELETE appointment (Admin only)</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = $"Appointment {id} not found." });
        return NoContent();
    }
}
