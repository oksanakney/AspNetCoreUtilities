using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AspNetCoreUtilities.Attributes
{
    public class IsBefore : ValidationAttribute // not an interface, has error message inside
    {
        private const string DateTimeFormat = "dd.MM.yyyy";
        private readonly DateTime date;

        // Se polzavat edno kam edno kato vgradeni validation attributes
        public IsBefore(string dateInput)
        {
            date = DateTime.ParseExact(dateInput, DateTimeFormat, CultureInfo.InvariantCulture);
        }

        public string DatePurpose { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value >= date)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
