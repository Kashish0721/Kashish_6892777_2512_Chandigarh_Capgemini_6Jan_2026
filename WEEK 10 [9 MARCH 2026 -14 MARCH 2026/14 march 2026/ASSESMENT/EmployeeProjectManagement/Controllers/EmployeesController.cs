using EmployeeProjectManagement.Data;
using EmployeeProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeProjectManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // ===============================
        // GET: Employees
        // ===============================
        public IActionResult Index()
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .ToList();

            return View(employees);
        }

        // ===============================
        // GET: Employees/Create
        // ===============================
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.Projects = _context.Projects.ToList();

            return View();
        }

        // ===============================
        // POST: Employees/Create
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee, int[] selectedProjects)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                // Insert into join table
                foreach (var projectId in selectedProjects)
                {
                    EmployeeProject ep = new EmployeeProject
                    {
                        EmployeeId = employee.EmployeeId,
                        ProjectId = projectId,
                        AssignedDate = DateTime.Now
                    };

                    _context.EmployeeProjects.Add(ep);
                }

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.Projects = _context.Projects.ToList();

            return View(employee);
        }

        // ===============================
        // GET: Employees/Edit
        // ===============================
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
                return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
            ViewBag.Projects = _context.Projects.ToList();

            return View(employee);
        }

        // ===============================
        // POST: Employees/Edit
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
            ViewBag.Projects = _context.Projects.ToList();

            return View(employee);
        }

        // ===============================
        // GET: Employees/Delete
        // ===============================
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // ===============================
        // POST: Employees/Delete
        // ===============================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}