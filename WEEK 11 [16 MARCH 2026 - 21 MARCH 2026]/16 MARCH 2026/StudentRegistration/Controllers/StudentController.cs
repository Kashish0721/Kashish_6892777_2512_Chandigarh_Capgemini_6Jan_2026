using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Models;
using System.Collections.Generic;

namespace StudentRegistration.Controllers
{
    public class StudentController : Controller
    {
        static List<Student> students = new List<Student>();
        static int id = 1;

        // Show Registration Form
        public IActionResult Register()
        {
            return View();
        }

        // Handle Form Submit
        [HttpPost]
        public IActionResult Register(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = id++;
                students.Add(student);

                TempData["Success"] = "Student Registered Successfully!";

                return RedirectToAction("Details", new { id = student.Id });
            }

            return View(student);
        }

        // Show Details Page
        public IActionResult Details(int id)
        {
            var student = students.Find(s => s.Id == id);
            return View(student);
        }
    }
}