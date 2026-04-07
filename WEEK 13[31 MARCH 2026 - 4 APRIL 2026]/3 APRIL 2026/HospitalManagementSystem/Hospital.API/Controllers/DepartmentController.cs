using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DepartmentController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Add(DepartmentDTO dto)
    {
        var dept = _mapper.Map<Department>(dto);
        _context.Departments.Add(dept);
        await _context.SaveChangesAsync();
        return Ok(dept);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.Departments.ToListAsync();
        return Ok(_mapper.Map<List<DepartmentDTO>>(data));
    }
}