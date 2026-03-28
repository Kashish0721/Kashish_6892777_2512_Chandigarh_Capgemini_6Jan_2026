using Microsoft.AspNetCore.Mvc;

namespace LoginSystemMVC.Controllers
{
    public class AccountController : Controller
    {
        // LOGIN PAGE
        public IActionResult Login()
        {
            return View();
        }

        // LOGIN POST
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                HttpContext.Session.SetString("user", username);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        }

        // DASHBOARD
        public IActionResult Dashboard()
        {
            var user = HttpContext.Session.GetString("user");

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Username = user;
            return View();
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
