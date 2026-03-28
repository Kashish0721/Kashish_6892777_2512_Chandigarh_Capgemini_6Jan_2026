using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ProductManagementMVC.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action Executing: " +
                context.ActionDescriptor.DisplayName +
                " Time: " + DateTime.Now);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action Executed: " +
                context.ActionDescriptor.DisplayName +
                " Time: " + DateTime.Now);
        }
    }
}