using System.Security.Claims;
using HealthcareSystem.API.Services.Interfaces;
using HealthcareSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _service;
    private readonly ILogger<DoctorsController> _logger;

    public DoctorsController(IDoctorService service, ILogger<DoctorsController> logger)
    { _service = service; _logger = logger; }

    /// <summary>GET all doctors with pagination/search (public)</summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<DoctorDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
        => Ok(await _service.GetAllAsync(parameters));

    /// <summary>GET doctor by ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DoctorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound(new { message = $"Doctor {id} not found." });
        return Ok(result);
    }

    /// <summary>GET current doctor's own profile</summary>
    [HttpGet("me")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.GetByUserIdAsync(userId);
        if (result == null) return NotFound(new { message = "Doctor profile not found." });
        return Ok(result);
    }

    /// <summary>GET doctors by specialization — /api/doctors?specializationId=1</summary>
    [HttpGet("by-specialization/{specializationId:int}")]
    [ProducesResponseType(typeof(IEnumerable<DoctorDto>), 200)]
    public async Task<IActionResult> GetBySpecialization([FromRoute] int specializationId)
        => Ok(await _service.GetBySpecializationAsync(specializationId));

    /// <summary>POST create doctor profile</summary>
    [HttpPost]
    [Authorize(Roles = "Doctor,Admin")]
    [ProducesResponseType(typeof(DoctorDto), 201)]
    public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>PUT full update of doctor</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Doctor,Admin")]
    [ProducesResponseType(typeof(DoctorDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDoctorDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdateAsync(id, dto);
        if (result == null) return NotFound(new { message = $"Doctor {id} not found." });
        return Ok(result);
    }

    /// <summary>DELETE doctor (Admin only)</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = $"Doctor {id} not found." });
        return NoContent();
    }
}
