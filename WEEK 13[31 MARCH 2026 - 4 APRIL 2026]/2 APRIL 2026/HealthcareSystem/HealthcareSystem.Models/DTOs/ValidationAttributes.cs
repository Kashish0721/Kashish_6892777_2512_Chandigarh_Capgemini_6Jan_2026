using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models.DTOs;

/// <summary>Validates appointment date is in the future and within 3 months</summary>
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is DateTime date)
        {
            if (date <= DateTime.Now)
                return new ValidationResult("Appointment date must be in the future.");
            if (date > DateTime.Now.AddMonths(3))
                return new ValidationResult("Appointment cannot be scheduled more than 3 months in advance.");
        }
        return ValidationResult.Success;
    }
}

/// <summary>Validates appointment time is within business hours (9 AM – 6 PM)</summary>
public class BusinessHoursAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is DateTime date)
        {
            if (date.Hour < 9 || date.Hour >= 18)
                return new ValidationResult("Appointments must be scheduled between 9:00 AM and 6:00 PM.");
        }
        return ValidationResult.Success;
    }
}

/// <summary>Validates password strength</summary>
public class StrongPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        var pwd = value as string;
        if (string.IsNullOrEmpty(pwd)) return ValidationResult.Success;

        if (pwd.Length < 6)
            return new ValidationResult("Password must be at least 6 characters.");
        if (!pwd.Any(char.IsUpper))
            return new ValidationResult("Password must contain at least one uppercase letter.");
        if (!pwd.Any(char.IsDigit))
            return new ValidationResult("Password must contain at least one digit.");

        return ValidationResult.Success;
    }
}
