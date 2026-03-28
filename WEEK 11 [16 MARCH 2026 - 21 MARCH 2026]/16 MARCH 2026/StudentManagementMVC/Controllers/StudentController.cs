using Microsoft.AspNetCore.Mvc;
using StudentManagementMVC.Models;
using System.Collections.Generic;

namespace StudentManagementMVC.Controllers
{
    public class StudentController : Controller
    {
        public static List<Student> students = new List<Student>();
        static int id = 1;

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = id++;
                students.Add(student);

                TempData["msg"] = "Student Added Successfully!";
                return RedirectToAction("Dashboard", "Account");
            }

            return View(student);
        }
    }
}