using HealthcareSystem.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicinesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicinesController(AppDbContext context) => _context = context;

    /// <summary>GET all medicines (for prescription form)</summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var medicines = await _context.Medicines
            .Select(m => new { m.Id, m.Name, m.Generic, m.Dosage, m.Description })
            .OrderBy(m => m.Name)
            .ToListAsync();
        return Ok(medicines);
    }

    /// <summary>GET medicine by ID</summary>
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var medicine = await _context.Medicines.FindAsync(id);
        if (medicine == null) return NotFound(new { message = $"Medicine {id} not found." });
        return Ok(medicine);
    }

    /// <summary>POST create medicine (Admin only)</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateMedicineDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var medicine = new HealthcareSystem.Models.Entities.Medicine
        {
            Name = dto.Name,
            Generic = dto.Generic,
            Dosage = dto.Dosage,
            Description = dto.Description
        };
        await _context.Medicines.AddAsync(medicine);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = medicine.Id }, medicine);
    }
}

public class CreateMedicineDto
{
    [System.ComponentModel.DataAnnotations.Required, System.ComponentModel.DataAnnotations.MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    public string? Generic { get; set; }
    public string? Dosage { get; set; }
    public string? Description { get; set; }
}
