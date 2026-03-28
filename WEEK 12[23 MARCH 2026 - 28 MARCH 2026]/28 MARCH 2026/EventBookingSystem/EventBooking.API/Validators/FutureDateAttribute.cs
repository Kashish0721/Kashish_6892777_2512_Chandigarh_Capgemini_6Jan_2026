using System.ComponentModel.DataAnnotations;

namespace EventBooking.API.Validators
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime > DateTime.UtcNow;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage ?? $"{name} must be a date in the future.";
        }
    }
}
