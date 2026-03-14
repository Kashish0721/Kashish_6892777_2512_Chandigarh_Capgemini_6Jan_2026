using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmpDbContext _context;

        public EmployeeController(EmpDbContext context)
        {
            _context = context;
        }

        // ================== INDEX ==================
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // ================== DETAILS ==================
        public IActionResult Details(int id)
        {
            var emp = _context.Employees.FirstOrDefault(e => e.Id == id);
            return View(emp);
        }

        // ================== CREATE (GET) ==================
        public IActionResult Create()
        {
            return View();
        }

        // ================== CREATE (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        // ================== EDIT (GET) ==================
        public IActionResult Edit(int id)
        {
            var emp = _context.Employees.Find(id);
            return View(emp);
        }

        // ================== EDIT (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(emp);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        // ================== DELETE (GET) ==================
        public IActionResult Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            return View(emp);
        }

        // ================== DELETE (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Employee emp)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}