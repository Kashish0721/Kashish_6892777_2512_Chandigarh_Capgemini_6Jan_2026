using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LearningPlatformAPI.Data;
using LearningPlatformAPI.Models;
using AutoMapper;

namespace LearningPlatformAPI.DTOs   // ✅ FIXED
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        // ✅ SINGLE CONSTRUCTOR (FIXED)
        public CoursesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL (DTO)
        [HttpGet]
        public IActionResult GetAll()
        {
            var courses = _context.Courses.ToList();
            var result = _mapper.Map<List<CourseDto>>(courses);

            return Ok(result);
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
                return NotFound();

            var result = _mapper.Map<CourseDto>(course);

            return Ok(result);
        }

        // ✅ CREATE COURSE (Instructor/Admin)
        [Authorize(Roles = "Instructor,Admin")]
        [HttpPost]
        public IActionResult Create(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);

            _context.Courses.Add(course);
            _context.SaveChanges();

            return Ok(_mapper.Map<CourseDto>(course));
        }

        // ✅ ADD LESSON
        [Authorize(Roles = "Instructor,Admin")]
        [HttpPost("{id}/lessons")]
        public IActionResult AddLesson(int id, LessonDto lessonDto)
        {
            var lesson = _mapper.Map<Lesson>(lessonDto);
            lesson.CourseId = id;

            _context.Lessons.Add(lesson);
            _context.SaveChanges();

            return Ok(lessonDto);
        }
    }
}