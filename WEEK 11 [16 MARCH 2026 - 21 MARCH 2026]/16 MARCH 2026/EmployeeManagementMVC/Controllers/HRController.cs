using Microsoft.AspNetCore.Mvc;
using EmployeeManagementMVC.Filters;
using EmployeeManagementMVC.Controllers;

namespace EmployeeManagementMVC.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))]
    public class HRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeeList()
        {
            return View(EmployeeController.employees);
        }

        public IActionResult Reports()
        {
            throw new Exception("Test Exception"); // for testing filter
        }
    }
}