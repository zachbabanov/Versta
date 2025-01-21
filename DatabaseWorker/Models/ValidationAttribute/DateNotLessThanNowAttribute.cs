using System.ComponentModel.DataAnnotations;

namespace DatabaseWorker.Models.ValidationAttribute
{
    public class DateNotLessThanNowAttribute: System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            ErrorMessage = "Дата не должна быть раньше, чем текущая!";

            if (value is DateTime date)
            {
                if (date < DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success!;
        }
    }
}
