using HealthcareSystem.Models.DTOs;
using HealthcareSystem.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.MVC.Controllers;

// ─── Base helper ─────────────────────────────────────────────────────────────
public abstract class BaseController : Controller
{
    protected string? UserRole => HttpContext.Session.GetString("UserRole");
    protected string? UserName => HttpContext.Session.GetString("UserName");
    protected int? UserId => HttpContext.Session.GetInt32("UserId");
    protected bool IsLoggedIn => HttpContext.Session.GetString("JwtToken") != null;

    protected IActionResult RequireLogin()
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Account");
        return null!;
    }

    protected IActionResult RequireRole(params string[] roles)
    {
        var check = RequireLogin();
        if (check != null) return check;
        if (!roles.Contains(UserRole))
            return RedirectToAction("AccessDenied", "Account");
        return null!;
    }
}

// ─── Home ─────────────────────────────────────────────────────────────────────
public class HomeController : BaseController
{
    private readonly ApiService _api;
    public HomeController(ApiService api) => _api = api;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Account");
        ViewBag.UserName = UserName;
        ViewBag.UserRole = UserRole;

        if (UserRole == "Admin")
        {
            var dashboard = await _api.GetDashboardAsync();
            return View("AdminDashboard", dashboard);
        }
        if (UserRole == "Doctor")
        {
            var appointments = await _api.GetMyAppointmentsAsync();
            return View("DoctorDashboard", appointments);
        }
        // Patient
        var myAppts = await _api.GetMyAppointmentsAsync();
        return View("PatientDashboard", myAppts);
    }
}

// ─── Patients ─────────────────────────────────────────────────────────────────
public class PatientsController : BaseController
{
    private readonly ApiService _api;
    public PatientsController(ApiService api) => _api = api;

    public async Task<IActionResult> Index(int page = 1, string? search = null)
    {
        var redirect = RequireRole("Admin", "Doctor");
        if (redirect != null) return redirect;
        var result = await _api.GetPatientsAsync(page, search);
        ViewBag.Search = search;
        return View(result);
    }

    public async Task<IActionResult> Details(int id)
    {
        var redirect = RequireLogin();
        if (redirect != null) return redirect;
        var patient = await _api.GetPatientByIdAsync(id);
        if (patient == null) return NotFound();
        return View(patient);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var redirect = RequireRole("Patient");
        if (redirect != null) return redirect;
        return View(new CreatePatientDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePatientDto model)
    {
        var redirect = RequireRole("Patient");
        if (redirect != null) return redirect;
        if (!ModelState.IsValid) return View(model);

        var result = await _api.CreatePatientProfileAsync(model);
        if (result == null) { ModelState.AddModelError("", "Failed to create profile."); return View(model); }
        TempData["Success"] = "Patient profile created successfully!";
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var redirect = RequireLogin();
        if (redirect != null) return redirect;
        var patient = await _api.GetPatientByIdAsync(id);
        if (patient == null) return NotFound();
        var dto = new UpdatePatientDto
        {
            PhoneNumber = patient.PhoneNumber, DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender, Address = patient.Address,
            MedicalHistory = patient.MedicalHistory, BloodGroup = patient.BloodGroup
        };
        ViewBag.PatientId = id;
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdatePatientDto model)
    {
        var redirect = RequireLogin();
        if (redirect != null) return redirect;
        if (!ModelState.IsValid) { ViewBag.PatientId = id; return View(model); }
        await _api.UpdatePatientAsync(id, model);
        TempData["Success"] = "Patient updated.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var redirect = RequireRole("Admin");
        if (redirect != null) return redirect;
        await _api.DeletePatientAsync(id);
        TempData["Success"] = "Patient deleted.";
        return RedirectToAction("Index");
    }
}

// ─── Doctors ──────────────────────────────────────────────────────────────────
public class DoctorsController : BaseController
{
    private readonly ApiService _api;
    public DoctorsController(ApiService api) => _api = api;

    public async Task<IActionResult> Index(int page = 1, string? search = null, int? specializationId = null)
    {
        IEnumerable<DoctorDto>? doctors = null;
        PagedResult<DoctorDto>? paged = null;

        if (specializationId.HasValue)
            doctors = await _api.GetDoctorsBySpecializationAsync(specializationId.Value);
        else
            paged = await _api.GetDoctorsAsync(page, search);

        var specs = await _api.GetSpecializationsAsync();
        ViewBag.Specializations = specs;
        ViewBag.Search = search;
        ViewBag.SpecializationId = specializationId;

        if (doctors != null)
            return View("IndexFiltered", doctors);

        return View(paged);
    }

    public async Task<IActionResult> Details(int id)
    {
        var doctor = await _api.GetDoctorByIdAsync(id);
        if (doctor == null) return NotFound();
        return View(doctor);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var redirect = RequireRole("Doctor");
        if (redirect != null) return redirect;
        ViewBag.Specializations = await _api.GetSpecializationsAsync();
        return View(new CreateDoctorDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateDoctorDto model)
    {
        var redirect = RequireRole("Doctor");
        if (redirect != null) return redirect;
        if (!ModelState.IsValid)
        {
            ViewBag.Specializations = await _api.GetSpecializationsAsync();
            return View(model);
        }
        var result = await _api.CreateDoctorProfileAsync(model);
        if (result == null) { ModelState.AddModelError("", "Failed."); return View(model); }
        TempData["Success"] = "Doctor profile created!";
        return RedirectToAction("Index", "Home");
    }
}

// ─── Appointments ─────────────────────────────────────────────────────────────
public class AppointmentsController : BaseController
{
    private readonly ApiService _api;
    public AppointmentsController(ApiService api) => _api = api;

    public async Task<IActionResult> Index(int page = 1)
    {
        var redirect = RequireLogin();
        if (redirect != null) return redirect;

        if (UserRole == "Patient")
        {
            var myAppts = await _api.GetMyAppointmentsAsync();
            return View("MyAppointments", myAppts);
        }

        var all = await _api.GetAppointmentsAsync(page);
        return View(all);
    }

    [HttpGet]
    public async Task<IActionResult> Book()
    {
        var redirect = RequireRole("Patient");
        if (redirect != null) return redirect;

            var patientProfile = await _api.GetMyPatientProfileAsync();
            if (patientProfile == null)
            {
                TempData["Error"] = "Complete your patient profile before booking an appointment.";
                return RedirectToAction("Create", "Patients");
            }

            ViewBag.Doctors = await _api.GetDoctorsAsync(1);
            ViewBag.Specializations = await _api.GetSpecializationsAsync();
            return View(new CreateAppointmentDto { AppointmentDate = DateTime.Now.AddDays(1) });
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(CreateAppointmentDto model)
    {
        var redirect = RequireRole("Patient");
        if (redirect != null) return redirect;

        var patientProfile = await _api.GetMyPatientProfileAsync();
        if (patientProfile == null)
        {
            TempData["Error"] = "Complete your patient profile before booking an appointment.";
            return RedirectToAction("Create", "Patients");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Doctors = await _api.GetDoctorsAsync(1);
            ViewBag.Specializations = await _api.GetSpecializationsAsync();
            return View(model);
        }

        var result = await _api.BookAppointmentAsync(model);
        if (result == null)
        {
            ModelState.AddModelError("", "Booking failed. Ensure your patient profile is complete.");
            ViewBag.Doctors = await _api.GetDoctorsAsync(1);
            ViewBag.Specializations = await _api.GetSpecializationsAsync();
            return View(model);
        }

        TempData["Success"] = "Appointment booked successfully!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        var redirect = RequireRole("Doctor", "Admin");
        if (redirect != null) return redirect;
        await _api.PatchAppointmentAsync(id, new PatchAppointmentDto { Status = status });
        TempData["Success"] = $"Appointment marked as {status}.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var redirect = RequireRole("Admin");
        if (redirect != null) return redirect;
        await _api.DeleteAppointmentAsync(id);
        TempData["Success"] = "Appointment deleted.";
        return RedirectToAction("Index");
    }
}

// ─── Bills ───────────────────────────────────────────────────────────────────
public class BillsController : BaseController
{
    private readonly ApiService _api;
    public BillsController(ApiService api) => _api = api;

    public async Task<IActionResult> Index()
    {
        var redirect = RequireRole("Admin", "Patient");
        if (redirect != null) return redirect;

        IEnumerable<BillDto>? bills = null;
        if (UserRole == "Admin")
        {
            bills = await _api.GetAllBillsAsync();
        }
        else
        {
            bills = await _api.GetMyBillsAsync();
        }

        return View(bills ?? Array.Empty<BillDto>());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBillDto model)
    {
        var redirect = RequireRole("Admin");
        if (redirect != null) return Json(new { success = false, message = "Unauthorized" });

        try
        {
            if (model == null || model.AppointmentId <= 0)
                return Json(new { success = false, message = "Invalid appointment ID" });

            var bill = await _api.CreateBillAsync(model);
            if (bill == null)
            {
                return Json(new { success = false, message = "Failed to create bill. Please check appointment ID and try again." });
            }
            return Json(new { success = true, message = "Bill created successfully!" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(int id)
    {
        var redirect = RequireRole("Admin", "Patient");
        if (redirect != null) return redirect;

        var bill = await _api.MarkBillAsPaidAsync(id);
        if (bill == null)
        {
            TempData["Error"] = "Unable to mark bill as paid.";
        }
        else
        {
            TempData["Success"] = "Bill payment updated successfully.";
        }

        return RedirectToAction("Index");
    }
}

// ─── Admin ────────────────────────────────────────────────────────────────────
public class AdminController : BaseController
{
    private readonly ApiService _api;
    public AdminController(ApiService api) => _api = api;

    public async Task<IActionResult> Users(int page = 1, string? search = null)
    {
        var redirect = RequireRole("Admin");
        if (redirect != null) return redirect;
        var result = await _api.GetUsersAsync(page, search);
        ViewBag.Search = search;
        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var redirect = RequireRole("Admin");
        if (redirect != null) return redirect;
        await _api.DeleteUserAsync(id);
        TempData["Success"] = "User deleted.";
        return RedirectToAction("Users");
    }
}
