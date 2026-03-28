using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagementMVC.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.GetString("user");

            Console.WriteLine($"[LOG] {context.ActionDescriptor.DisplayName} | User: {user} | Time: {DateTime.Now}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
