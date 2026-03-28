using Microsoft.AspNetCore.Mvc;
using EmployeeManagementMVC.Models;
using System.Collections.Generic;

namespace EmployeeManagementMVC.Controllers
{
    public class EmployeeController : Controller
    {
        public static List<Employee> employees = new List<Employee>();
        static int id = 1;

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Employee emp)
        {
            if (ModelState.IsValid)
            {
                emp.Id = id++;
                employees.Add(emp);

                TempData["msg"] = "Employee Registered!";
                return RedirectToAction("Details", new { id = emp.Id });
            }

            return View(emp);
        }

        public IActionResult Details(int id)
        {
            var emp = employees.Find(e => e.Id == id);
            return View(emp);
        }
    }
}