using AspNetCoreUtilities.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreUtilities.Models
{
    public class HomeViewModel : IValidatableObject // slozhna zavisimost, v otlichie ot : ValidationAttribute ne e preizpolzvaem
    {
        // ctor of attribite becomes this, every public prop can be written like DatePurpose and it takes its value
        [IsBefore("30.04.2024", ErrorMessage = "Date must be before 30.04.2024", DatePurpose = "Hello")]
        public DateTime MyDate { get; set; }

        [IsAdult(18, ErrorMessage = "Must be at least 18 years old")]
        public DateTime Birthday { get; set; }

        public string? Name { get; set; }
        public string? Country { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required");
            }

            if (string.IsNullOrEmpty(Country))
            {
                yield return new ValidationResult("Name is required");
            }

            // Slozhna zavisimost
            if (Name == "Pesho" && Country != "BG")
            {
                yield return new ValidationResult("If name is Pesho, Country must be BG");
            }
        }
    }
}
