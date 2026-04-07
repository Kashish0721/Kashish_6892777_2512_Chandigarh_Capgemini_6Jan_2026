using HealthcareSystem.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.MVC.Controllers;

public class PrescriptionsController : BaseController
{
    private readonly ApiService _api;
    public PrescriptionsController(ApiService api) => _api = api;

    public async Task<IActionResult> My()
    {
        var redirect = RequireRole("Patient");
        if (redirect != null) return redirect;

        var prescriptions = await _api.GetMyPrescriptionsAsync();
        return View(prescriptions);
    }

    public async Task<IActionResult> Details(int id)
    {
        var redirect = RequireLogin();
        if (redirect != null) return redirect;

        // Would call GetPrescriptionByIdAsync if implemented in ApiService
        return RedirectToAction("My");
    }
}
