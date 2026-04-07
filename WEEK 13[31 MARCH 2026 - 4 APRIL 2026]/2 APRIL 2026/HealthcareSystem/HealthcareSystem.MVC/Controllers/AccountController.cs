using HealthcareSystem.Models.DTOs;
using HealthcareSystem.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.MVC.Controllers;

public class AccountController : Controller
{
    private readonly ApiService _api;

    public AccountController(ApiService api) => _api = api;

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (HttpContext.Session.GetString("JwtToken") != null)
            return RedirectToAction("Index", "Home");

        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _api.LoginAsync(model);
        if (result == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        // Store token in session
        HttpContext.Session.SetString("JwtToken", result.Token);
        HttpContext.Session.SetString("RefreshToken", result.RefreshToken);
        HttpContext.Session.SetString("UserRole", result.Role);
        HttpContext.Session.SetString("UserName", result.FullName);
        HttpContext.Session.SetInt32("UserId", result.UserId);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _api.RegisterAsync(model);
        if (result == null)
        {
            ModelState.AddModelError("Email", "Email already in use.");
            return View(model);
        }

        HttpContext.Session.SetString("JwtToken", result.Token);
        HttpContext.Session.SetString("RefreshToken", result.RefreshToken);
        HttpContext.Session.SetString("UserRole", result.Role);
        HttpContext.Session.SetString("UserName", result.FullName);
        HttpContext.Session.SetInt32("UserId", result.UserId);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();
}
