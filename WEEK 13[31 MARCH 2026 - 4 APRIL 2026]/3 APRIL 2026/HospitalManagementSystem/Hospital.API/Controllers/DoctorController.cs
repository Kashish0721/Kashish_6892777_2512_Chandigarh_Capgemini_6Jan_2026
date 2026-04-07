using Microsoft.AspNetCore.Mvc;
using AutoMapper;
[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DoctorController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Add(DoctorDTO dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return Ok(doctor);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _context.Doctors
            .Include(d => d.Department)
            .Include(d => d.User)
            .ToListAsync();

        return Ok(doctors);
    }
}