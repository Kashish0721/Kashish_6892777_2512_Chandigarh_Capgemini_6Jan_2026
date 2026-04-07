using Microsoft.AspNetCore.Mvc;
[HttpPost]
public async Task<IActionResult> Book(Appointment appointment)
{
    appointment.Status = "Booked";

    _context.Appointments.Add(appointment);
    await _context.SaveChangesAsync();

    return Ok("Appointment Booked");
}