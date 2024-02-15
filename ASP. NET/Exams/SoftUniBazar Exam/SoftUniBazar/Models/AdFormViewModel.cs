using SoftUniBazar.Data.Constants;
using SoftUniBazar.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Models
{
    public class AdFormViewModel
    {
        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.AdNameMaxLength, 
            MinimumLength = ValidationConstants.AdNameMinLength, 
            ErrorMessage = ErrorMessagesConstants.StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.AdDescriptionMaxLength,
            MinimumLength = ValidationConstants.AdDescriptionMinLength,
            ErrorMessage = ErrorMessagesConstants.StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
