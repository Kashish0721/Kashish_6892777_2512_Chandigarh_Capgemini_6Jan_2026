using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Web.Pages.Bookings
{
    [Authorize(Roles = "Admin")]
    public class AllBookingsModel : PageModel
    {
        private readonly IConfiguration _config;
        public string ApiBaseUrl { get; private set; } = string.Empty;

        public AllBookingsModel(IConfiguration config) => _config = config;

        public void OnGet()
        {
            ApiBaseUrl = _config["ApiSettings:BaseUrl"] ?? "https://localhost:7000";
        }
    }
}
