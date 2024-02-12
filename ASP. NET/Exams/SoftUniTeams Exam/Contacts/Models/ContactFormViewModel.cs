using Contacts.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.ContactFirstNameMaxLength,
            MinimumLength = ValidationConstants.ContactFirstNameMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.ContactLastNameMaxLength,
            MinimumLength = ValidationConstants.ContactLastNameMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.EmailMaxLength,
            MinimumLength = ValidationConstants.EmailMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.ContactPhoneMaxLength,
            MinimumLength = ValidationConstants.ContactPhoneMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        [RegularExpression(ValidationConstants.ContactPhoneRegularExpression,
            ErrorMessage = ErrorMessageConstants.RegularExpressionErrorMessage)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [RegularExpression(ValidationConstants.ContactWebsiteRegularExpression,
            ErrorMessage = ErrorMessageConstants.RegularExpressionErrorMessage)]
        public string Website { get; set; }
    }
}
