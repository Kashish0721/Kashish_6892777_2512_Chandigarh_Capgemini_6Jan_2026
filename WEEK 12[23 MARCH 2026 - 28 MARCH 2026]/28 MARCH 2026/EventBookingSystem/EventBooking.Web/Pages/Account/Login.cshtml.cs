using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace EventBooking.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _config;
        public string ApiBaseUrl { get; private set; } = string.Empty;
        public string ReturnUrl { get; private set; } = string.Empty;

        public LoginModel(IConfiguration config) => _config = config;

        public IActionResult OnGet(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToPage("/Events/Index");

            ApiBaseUrl = _config["ApiSettings:BaseUrl"] ?? "https://localhost:7000";
            ReturnUrl = returnUrl ?? "/Events/Index";
            return Page();
        }

        // Called by the JS fetch after successful API login
        public async Task<IActionResult> OnPostLoginServerAsync()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            try
            {
                var payload = JsonSerializer.Deserialize<ServerLoginPayload>(body,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (payload == null) return BadRequest();

                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, payload.UserId.ToString()),
                    new(ClaimTypes.Name, payload.FullName),
                    new(ClaimTypes.Email, payload.Email),
                    new(ClaimTypes.Role, payload.Role),
                    new("jwt_token", payload.Token)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) });

                return new JsonResult(new { success = true });
            }
            catch
            {
                return BadRequest();
            }
        }

        private record ServerLoginPayload(string Token, string FullName, string Email, string Role, int UserId);
    }
}
