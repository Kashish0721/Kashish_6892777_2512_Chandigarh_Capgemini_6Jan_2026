using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages.Events
{
    public class EventDetailsModel : PageModel
    {
        private readonly IConfiguration _config;
        public string ApiBaseUrl { get; private set; } = string.Empty;
        public int EventId { get; private set; }

        public EventDetailsModel(IConfiguration config) => _config = config;

        public IActionResult OnGet(int id)
        {
            if (id <= 0) return RedirectToPage("/Events/Index");
            EventId = id;
            ApiBaseUrl = _config["ApiSettings:BaseUrl"] ?? "https://localhost:7000";
            return Page();
        }
    }
}
