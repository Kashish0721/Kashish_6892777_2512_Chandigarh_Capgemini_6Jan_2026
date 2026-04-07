using System.Security.Claims;
using HealthcareSystem.API.Services.Interfaces;
using HealthcareSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _service;
    private readonly ILogger<PatientsController> _logger;

    public PatientsController(IPatientService service, ILogger<PatientsController> logger)
    { _service = service; _logger = logger; }

    /// <summary>GET all patients (Admin only) with pagination and search</summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(PagedResult<PatientDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
    {
        var result = await _service.GetAllAsync(parameters);
        return Ok(result);
    }

    /// <summary>GET patient by ID</summary>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    [ProducesResponseType(typeof(PatientDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound(new { message = $"Patient {id} not found." });
        return Ok(result);
    }

    /// <summary>GET current patient's own profile</summary>
    [HttpGet("me")]
    [Authorize(Roles = "Patient")]
    [ProducesResponseType(typeof(PatientDto), 200)]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.GetByUserIdAsync(userId);
        if (result == null) return NotFound(new { message = "Patient profile not found. Please complete setup." });
        return Ok(result);
    }

    /// <summary>POST create patient profile</summary>
    [HttpPost]
    [Authorize(Roles = "Patient,Admin")]
    [ProducesResponseType(typeof(PatientDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>PUT full update of patient</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Patient,Admin")]
    [ProducesResponseType(typeof(PatientDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePatientDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _service.UpdateAsync(id, dto);
        if (result == null) return NotFound(new { message = $"Patient {id} not found." });
        return Ok(result);
    }

    /// <summary>DELETE patient (Admin only)</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = $"Patient {id} not found." });
        return NoContent();
    }
}
