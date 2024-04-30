using AspNetCoreUtilities.Attributes;

namespace AspNetCoreUtilities.Models
{
    public class HomeViewModel
    {
        // ctor of attribite becomes this, every public prop can be written like DatePurpose and it takes its value
        [IsBefore("30.04.2024", ErrorMessage = "Date must be before 30.04.2024", DatePurpose = "Hello")]
        public DateTime MyDate { get; set; }

        [IsAdult(18, ErrorMessage = "Must be at least 18 years old")]
        public DateTime Birthday { get; set; }
    }
}
