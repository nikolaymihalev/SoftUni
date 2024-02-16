using Homies.Data.Constants;
using System.ComponentModel.DataAnnotations;
using Type = Homies.Data.Models.Type;
namespace Homies.Models
{
    public class EventFormViewModel
    {
        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.EventNameMaxLength,
            MinimumLength = ValidationConstants.EventNameMinLength,
            ErrorMessage = ErrorMessagesConstants.StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.EventDescriptionMaxLength,
            MinimumLength = ValidationConstants.EventDescriptionMinLength,
            ErrorMessage = ErrorMessagesConstants.StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        public string Start { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        public string End { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        public int TypeId { get; set; }
        public IEnumerable<Type> Types { get; set; } = new List<Type>();
    }
}
