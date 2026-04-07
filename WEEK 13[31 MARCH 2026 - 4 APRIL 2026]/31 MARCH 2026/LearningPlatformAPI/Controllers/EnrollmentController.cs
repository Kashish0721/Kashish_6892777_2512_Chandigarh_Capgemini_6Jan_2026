using Microsoft.AspNetCore.Mvc;
using LearningPlatformAPI.Data;
using LearningPlatformAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace LearningPlatformAPI.Controllers
{
    [ApiController]
    [Route("api/v1/enroll")]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ ENROLL COURSE (Student)
        [Authorize(Roles = "Student")]
        [HttpPost]
        public IActionResult Enroll(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            return Ok("Enrolled Successfully");
        }
    }
}