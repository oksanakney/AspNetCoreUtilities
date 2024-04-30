using System.ComponentModel.DataAnnotations;

namespace AspNetCoreUtilities.Attributes
{
    public class IsAdult : ValidationAttribute
    {
        private readonly DateTime minimunAge = DateTime.Today.AddYears(-18);
        private readonly int adultAge = 18;

        public IsAdult(int age)
        {
            adultAge = age;
            minimunAge = DateTime.Today.AddYears(age * -1);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value <= minimunAge) 
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
