using System.Security.Claims;
using HealthcareSystem.API.Services.Interfaces;
using HealthcareSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BillsController : ControllerBase
{
    private readonly IBillService _service;
    private readonly IPatientService _patientService;
    private readonly ILogger<BillsController> _logger;

    public BillsController(IBillService service, IPatientService patientService, ILogger<BillsController> logger)
    {
        _service = service;
        _patientService = patientService;
        _logger = logger;
    }

    /// <summary>GET all bills (Admin)</summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<BillDto>), 200)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Fetching all bills");
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>GET bill by ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BillDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = $"Bill {id} not found." });
        return Ok(result);
    }

    /// <summary>GET my bills (Patient)</summary>
    [HttpGet("my")]
    [Authorize(Roles = "Patient")]
    [ProducesResponseType(typeof(IEnumerable<BillDto>), 200)]
    public async Task<IActionResult> GetMyBills()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var patient = await _patientService.GetByUserIdAsync(userId);
        if (patient == null)
            return NotFound(new { message = "Patient profile not found." });

        _logger.LogInformation("Fetching bills for patient {PatientId}", patient.Id);
        var result = await _service.GetByPatientAsync(patient.Id);
        return Ok(result);
    }

    /// <summary>GET bills by appointment</summary>
    [HttpGet("appointment/{appointmentId:int}")]
    [ProducesResponseType(typeof(BillDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByAppointment([FromRoute] int appointmentId)
    {
        var result = await _service.GetByAppointmentAsync(appointmentId);
        if (result == null)
            return NotFound(new { message = $"Bill for appointment {appointmentId} not found." });
        return Ok(result);
    }

    /// <summary>POST create new bill (Admin/System)</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(BillDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreateBillDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            _logger.LogInformation("Creating bill for appointment {AppointmentId}", dto.AppointmentId);
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Bill creation failed: {Message}", ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating bill for appointment {AppointmentId}", dto.AppointmentId);
            return BadRequest(new { message = "Failed to create bill. " + ex.Message });
        }
    }

    /// <summary>PUT update bill (Admin)</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(BillDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBillDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation("Updating bill {BillId}", id);
        var result = await _service.UpdateAsync(id, dto);
        if (result == null)
            return NotFound(new { message = $"Bill {id} not found." });
        return Ok(result);
    }

    /// <summary>PUT mark bill as paid</summary>
    [HttpPut("{id:int}/pay")]
    [Authorize(Roles = "Patient,Admin")]
    [ProducesResponseType(typeof(BillDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> MarkAsPaid([FromRoute] int id)
    {
        _logger.LogInformation("Marking bill {BillId} as paid", id);
        var result = await _service.MarkAsPaidAsync(id);
        if (result == null)
            return NotFound(new { message = $"Bill {id} not found." });
        return Ok(result);
    }

    /// <summary>DELETE bill (Admin)</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        _logger.LogInformation("Deleting bill {BillId}", id);
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Bill {id} not found." });
        return NoContent();
    }
}
