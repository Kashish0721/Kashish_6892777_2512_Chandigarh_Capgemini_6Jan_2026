using Microsoft.AspNetCore.Mvc;
using ProductManagementMVC.Models;
using System.Collections.Generic;

namespace ProductManagementMVC.Controllers
{
    public class StudentController : Controller
    {
        static List<Student> students = new List<Student>();

        // Show Registration Page
        public IActionResult Register()
        {
            return View();
        }

        // Handle Form Submit
        [HttpPost]
        public IActionResult Register(Student student)
        {
            students.Add(student);

            ViewBag.message = "Student Registered Successfully!";

            return View();
        }
    }
}