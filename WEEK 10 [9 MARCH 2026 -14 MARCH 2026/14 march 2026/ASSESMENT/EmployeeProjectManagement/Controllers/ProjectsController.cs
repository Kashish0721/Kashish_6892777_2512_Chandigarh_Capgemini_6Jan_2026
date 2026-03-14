using EmployeeProjectManagement.Data;
using EmployeeProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProjectManagement.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: Projects
        // =========================
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        // =========================
        // GET: Create
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // POST: Create
        // =========================
        [HttpPost]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(project);
        }

        // =========================
        // GET: Edit
        // =========================
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.Find(id);
            return View(project);
        }

        // =========================
        // POST: Edit
        // =========================
        [HttpPost]
        public IActionResult Edit(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // GET: Delete
        // =========================
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.Find(id);
            return View(project);
        }

        // =========================
        // POST: Delete
        // =========================
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = _context.Projects.Find(id);

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}