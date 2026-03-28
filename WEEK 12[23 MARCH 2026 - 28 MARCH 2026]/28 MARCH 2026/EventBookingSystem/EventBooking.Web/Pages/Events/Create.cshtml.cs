using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages.Events
{
    [Authorize(Roles = "Admin")]
    public class CreateEventModel : PageModel
    {
        private readonly IConfiguration _config;
        public string ApiBaseUrl { get; private set; } = string.Empty;

        public CreateEventModel(IConfiguration config) => _config = config;

        public void OnGet()
        {
            ApiBaseUrl = _config["ApiSettings:BaseUrl"] ?? "https://localhost:7000";
        }
    }
}
