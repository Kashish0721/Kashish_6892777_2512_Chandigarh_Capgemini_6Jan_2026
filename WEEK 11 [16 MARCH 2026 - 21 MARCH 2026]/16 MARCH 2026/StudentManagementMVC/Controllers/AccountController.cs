using Microsoft.AspNetCore.Mvc;

namespace StudentManagementMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                HttpContext.Session.SetString("user", username);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid Credentials";
            return View();
        }

        public IActionResult Dashboard()
        {
            var user = HttpContext.Session.GetString("user");

            if (user == null)
                return RedirectToAction("Login");

            ViewBag.Username = user;

            ViewBag.Students = StudentController.students;

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}