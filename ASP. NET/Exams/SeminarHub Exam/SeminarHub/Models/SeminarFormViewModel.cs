using SeminarHub.Data.Constants;
using SeminarHub.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
    /// <summary>
    /// View model for adding seminar 
    /// </summary>
    public class SeminarFormViewModel
    {
        /// <summary>
        /// Seminar's topic 
        /// </summary>
        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.SeminarTopicMaxLength,
            MinimumLength = ValidationConstants.SeminarTopicMinLength,
            ErrorMessage = ErrorMessagesConstants.StringLengthErrorMessage)]
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// Seminar's lecturer 
        /// </summary>
        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.SeminarLecturerMaxLength,
            MinimumLength = ValidationConstants.SeminarLecturerMinLength,
            ErrorMessage = ErrorMessagesConstants.StringLengthErrorMessage)]
        public string Lecturer { get; set; } = string.Empty;

        /// <summary>
        /// Seminar's details
        /// </summary>
        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.SeminarDetailsMaxLength,
            MinimumLength = ValidationConstants.SeminarDetailsMinLength,
            ErrorMessage = ErrorMessagesConstants.StringLengthErrorMessage)]
        public string Details { get; set; } = string.Empty;

        /// <summary>
        /// Seminar's date and time
        /// </summary>
        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        public string DateAndTime { get; set; } = string.Empty;


        /// <summary>
        /// Seminar's duration
        /// </summary>
        [Range(ValidationConstants.SeminarDurationMinValue,ValidationConstants.SeminarDurationMaxValue)]
        public int Duration { get; set; }


        /// <summary>
        /// Seminar's category identifier
        /// </summary>
        [Required(ErrorMessage = ErrorMessagesConstants.RequireErrorMessage)]
        public int CategoryId { get; set; }

        /// <summary>
        /// Collection for all kinds of category
        /// </summary>
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
