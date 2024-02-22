using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AspNetCoreAdvancedDemo.Attributes
{
    public class IsBefore : ValidationAttribute
    {
        private const string DateTimeFormat = "dd/MM/yyyy";
        private readonly DateTime date;
        public IsBefore(string dateInput)
        {
            date = DateTime.ParseExact(dateInput, DateTimeFormat, CultureInfo.InvariantCulture);
        }
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value >= date) 
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
