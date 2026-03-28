using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _config;
        public string ApiBaseUrl { get; private set; } = string.Empty;

        public RegisterModel(IConfiguration config) => _config = config;

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToPage("/Events/Index");

            ApiBaseUrl = _config["ApiSettings:BaseUrl"] ?? "https://localhost:5000";
            return Page();
        }
    }
}
