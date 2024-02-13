using Library.Data.Constants;
using Library.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookFormViewModel
    {
        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.BookTitleMaxLength,
            MinimumLength = ValidationConstants.BookTitleMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.BookAuthorMaxLength,
            MinimumLength = ValidationConstants.BookAuthorMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.BookDescriptionMaxLength,
            MinimumLength = ValidationConstants.BookDescriptionMinLength,
            ErrorMessage = ErrorMessageConstants.StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        [Range(0,10)]
        public decimal Rating { get; set; } 

        [Required(ErrorMessage = ErrorMessageConstants.RequireErrorMessage)]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
