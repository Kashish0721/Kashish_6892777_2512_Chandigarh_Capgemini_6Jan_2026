using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages.Events
{
    public class EventsIndexModel : PageModel
    {
        private readonly IConfiguration _config;
        public string ApiBaseUrl { get; private set; } = string.Empty;

        public EventsIndexModel(IConfiguration config) => _config = config;

        public void OnGet()
        {
            ApiBaseUrl = _config["ApiSettings:BaseUrl"] ?? "https://localhost:7000";
        }
    }
}
