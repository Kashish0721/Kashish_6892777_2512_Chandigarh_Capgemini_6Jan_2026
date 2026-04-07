using System.Security.Claims;
using HealthcareSystem.API.Services.Interfaces;
using HealthcareSystem.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.API.Controllers;

// ─── Specializations ──────────────────────────────────────────────────────────
[ApiController]
[Route("api/[controller]")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationService _service;

    public SpecializationsController(ISpecializationService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound(new { message = $"Specialization {id} not found." });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSpecializationDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = $"Specialization {id} not found." });
        return NoContent();
    }
}

// ─── Prescriptions ────────────────────────────────────────────────────────────
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _service;
    private readonly IPatientService _patientService;

    public PrescriptionsController(IPrescriptionService service, IPatientService patientService)
    { _service = service; _patientService = patientService; }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound(new { message = $"Prescription {id} not found." });
        return Ok(result);
    }

    [HttpGet("my")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetMyPrescriptions()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var patient = await _patientService.GetByUserIdAsync(userId);
        if (patient == null) return NotFound(new { message = "Patient profile not found." });
        return Ok(await _service.GetByPatientAsync(patient.Id));
    }

    [HttpGet("doctor")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetMyDoctorPrescriptions()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        // resolve doctor id from userId via claim or service — simplified here
        return Ok(await _service.GetByDoctorAsync(userId));
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    [ProducesResponseType(typeof(PrescriptionDto), 201)]
    public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}

// ─── Dashboard ────────────────────────────────────────────────────────────────
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _service;

    public DashboardController(IDashboardService service) => _service = service;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(DashboardDto), 200)]
    public async Task<IActionResult> Get() => Ok(await _service.GetDashboardAsync());
}

// ─── Admin: Users ─────────────────────────────────────────────────────────────
[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly HealthcareSystem.API.Repositories.Interfaces.IUserRepository _repo;
    private readonly AutoMapper.IMapper _mapper;

    public UsersController(HealthcareSystem.API.Repositories.Interfaces.IUserRepository repo,
        AutoMapper.IMapper mapper)
    { _repo = repo; _mapper = mapper; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
    {
        var result = await _repo.GetPagedAsync(parameters);
        return Ok(new PagedResult<UserDto>
        {
            Items = _mapper.Map<List<UserDto>>(result.Items),
            TotalCount = result.TotalCount, Page = result.Page, PageSize = result.PageSize
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto dto)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return NotFound();
        _mapper.Map(dto, user);
        await _repo.UpdateAsync(user);
        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return NotFound();
        await _repo.DeleteAsync(user);
        return NoContent();
    }
}
