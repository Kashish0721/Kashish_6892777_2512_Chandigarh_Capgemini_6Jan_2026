using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public IActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // 🔥 YAHI likhna hai
            if (username == "admin" && password == "admin123")
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
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}