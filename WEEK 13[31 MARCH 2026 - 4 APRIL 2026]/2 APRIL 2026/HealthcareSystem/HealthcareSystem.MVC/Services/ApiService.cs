using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using HealthcareSystem.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace HealthcareSystem.MVC.Services;

public class ApiService
{
    private readonly HttpClient _http;
    private readonly IHttpContextAccessor _ctx;
    private readonly JsonSerializerOptions _jsonOpts = new() { PropertyNameCaseInsensitive = true };

   public ApiService(HttpClient http, IHttpContextAccessor ctx, IConfiguration config)
{
    _http = http;
    _ctx = ctx;
    _http.BaseAddress = new Uri(config["ApiBaseUrl"]);
}
    private void AttachToken()
    {
        var token = _ctx.HttpContext?.Session.GetString("JwtToken");
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private StringContent Json<T>(T obj) =>
        new(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

    private async Task<T?> ReadAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) return default;
        return JsonSerializer.Deserialize<T>(content, _jsonOpts);
    }

    // ─── Auth ──────────────────────────────────────────────────────────────────
    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var resp = await _http.PostAsync("api/auth/login", Json(dto));
        return await ReadAsync<AuthResponseDto>(resp);
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        var resp = await _http.PostAsync("api/auth/register", Json(dto));
        return await ReadAsync<AuthResponseDto>(resp);
    }

    // ─── Dashboard ─────────────────────────────────────────────────────────────
    public async Task<DashboardDto?> GetDashboardAsync()
    {
        AttachToken();
        var resp = await _http.GetAsync("api/dashboard");
        return await ReadAsync<DashboardDto>(resp);
    }

    // ─── Patients ──────────────────────────────────────────────────────────────
    public async Task<PagedResult<PatientDto>?> GetPatientsAsync(int page = 1, string? search = null)
    {
        AttachToken();
        var url = $"api/patients?page={page}&pageSize=10{(search != null ? "&search=" + search : "")}";
        var resp = await _http.GetAsync(url);
        return await ReadAsync<PagedResult<PatientDto>>(resp);
    }

    public async Task<PatientDto?> GetPatientByIdAsync(int id)
    {
        AttachToken();
        var resp = await _http.GetAsync($"api/patients/{id}");
        return await ReadAsync<PatientDto>(resp);
    }

    public async Task<PatientDto?> GetMyPatientProfileAsync()
    {
        AttachToken();
        var resp = await _http.GetAsync("api/patients/me");
        return await ReadAsync<PatientDto>(resp);
    }

    public async Task<PatientDto?> CreatePatientProfileAsync(CreatePatientDto dto)
    {
        AttachToken();
        var resp = await _http.PostAsync("api/patients", Json(dto));
        return await ReadAsync<PatientDto>(resp);
    }

    public async Task<PatientDto?> UpdatePatientAsync(int id, UpdatePatientDto dto)
    {
        AttachToken();
        var resp = await _http.PutAsync($"api/patients/{id}", Json(dto));
        return await ReadAsync<PatientDto>(resp);
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        AttachToken();
        var resp = await _http.DeleteAsync($"api/patients/{id}");
        return resp.IsSuccessStatusCode;
    }

    // ─── Doctors ───────────────────────────────────────────────────────────────
    public async Task<PagedResult<DoctorDto>?> GetDoctorsAsync(int page = 1, string? search = null)
    {
        var url = $"api/doctors?page={page}&pageSize=10{(search != null ? "&search=" + search : "")}";
        var resp = await _http.GetAsync(url);
        return await ReadAsync<PagedResult<DoctorDto>>(resp);
    }

    public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
    {
        var resp = await _http.GetAsync($"api/doctors/{id}");
        return await ReadAsync<DoctorDto>(resp);
    }

    public async Task<IEnumerable<DoctorDto>?> GetDoctorsBySpecializationAsync(int specializationId)
    {
        var resp = await _http.GetAsync($"api/doctors/by-specialization/{specializationId}");
        return await ReadAsync<IEnumerable<DoctorDto>>(resp);
    }

    public async Task<DoctorDto?> CreateDoctorProfileAsync(CreateDoctorDto dto)
    {
        AttachToken();
        var resp = await _http.PostAsync("api/doctors", Json(dto));
        return await ReadAsync<DoctorDto>(resp);
    }

    public async Task<DoctorDto?> UpdateDoctorAsync(int id, UpdateDoctorDto dto)
    {
        AttachToken();
        var resp = await _http.PutAsync($"api/doctors/{id}", Json(dto));
        return await ReadAsync<DoctorDto>(resp);
    }

    // ─── Appointments ──────────────────────────────────────────────────────────
    public async Task<PagedResult<AppointmentDto>?> GetAppointmentsAsync(int page = 1)
    {
        AttachToken();
        var resp = await _http.GetAsync($"api/appointments?page={page}&pageSize=10");
        return await ReadAsync<PagedResult<AppointmentDto>>(resp);
    }

    public async Task<IEnumerable<AppointmentDto>?> GetMyAppointmentsAsync()
    {
        AttachToken();
        var resp = await _http.GetAsync("api/appointments/my");
        return await ReadAsync<IEnumerable<AppointmentDto>>(resp);
    }

    public async Task<AppointmentDto?> BookAppointmentAsync(CreateAppointmentDto dto)
    {
        AttachToken();
        var resp = await _http.PostAsync("api/appointments", Json(dto));
        return await ReadAsync<AppointmentDto>(resp);
    }

    public async Task<AppointmentDto?> PatchAppointmentAsync(int id, PatchAppointmentDto dto)
    {
        AttachToken();
        var request = new HttpRequestMessage(HttpMethod.Patch, $"api/appointments/{id}")
        { Content = Json(dto) };
        var resp = await _http.SendAsync(request);
        return await ReadAsync<AppointmentDto>(resp);
    }

    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        AttachToken();
        var resp = await _http.DeleteAsync($"api/appointments/{id}");
        return resp.IsSuccessStatusCode;
    }

    // ─── Specializations ───────────────────────────────────────────────────────
    public async Task<IEnumerable<SpecializationDto>?> GetSpecializationsAsync()
    {
        var resp = await _http.GetAsync("api/specializations");
        return await ReadAsync<IEnumerable<SpecializationDto>>(resp);
    }

    // ─── Prescriptions ─────────────────────────────────────────────────────────
    public async Task<IEnumerable<PrescriptionDto>?> GetMyPrescriptionsAsync()
    {
        AttachToken();
        var resp = await _http.GetAsync("api/prescriptions/my");
        return await ReadAsync<IEnumerable<PrescriptionDto>>(resp);
    }

    public async Task<PrescriptionDto?> CreatePrescriptionAsync(CreatePrescriptionDto dto)
    {
        AttachToken();
        var resp = await _http.PostAsync("api/prescriptions", Json(dto));
        return await ReadAsync<PrescriptionDto>(resp);
    }

    // ─── Bills ─────────────────────────────────────────────────────────────────
    public async Task<IEnumerable<BillDto>?> GetMyBillsAsync()
    {
        AttachToken();
        var resp = await _http.GetAsync("api/bills/my");
        return await ReadAsync<IEnumerable<BillDto>>(resp);
    }

    public async Task<IEnumerable<BillDto>?> GetAllBillsAsync()
    {
        AttachToken();
        var resp = await _http.GetAsync("api/bills");
        return await ReadAsync<IEnumerable<BillDto>>(resp);
    }

    public async Task<BillDto?> CreateBillAsync(CreateBillDto dto)
    {
        AttachToken();
        var resp = await _http.PostAsync("api/bills", Json(dto));
        return await ReadAsync<BillDto>(resp);
    }

    public async Task<BillDto?> MarkBillAsPaidAsync(int id)
    {
        AttachToken();
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/bills/{id}/pay");
        request.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        var resp = await _http.SendAsync(request);
        return await ReadAsync<BillDto>(resp);
    }

    // ─── Admin: Users ──────────────────────────────────────────────────────────
    public async Task<PagedResult<UserDto>?> GetUsersAsync(int page = 1, string? search = null)
    {
        AttachToken();
        var url = $"api/admin/users?page={page}&pageSize=10{(search != null ? "&search=" + search : "")}";
        var resp = await _http.GetAsync(url);
        return await ReadAsync<PagedResult<UserDto>>(resp);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        AttachToken();
        var resp = await _http.DeleteAsync($"api/admin/users/{id}");
        return resp.IsSuccessStatusCode;
    }
}
