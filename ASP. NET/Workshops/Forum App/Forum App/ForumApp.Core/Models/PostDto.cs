using ForumApp.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Core.Models
{
    /// <summary>
    /// Post data transfer object
    /// </summary>
    public class PostDto
    {
        /// <summary>
        /// Post identificator
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Post title
        /// </summary>
        [Required(ErrorMessage = ValidationConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.TitleMaxLength, 
            MinimumLength = ValidationConstants.TitleMinLength, 
            ErrorMessage = ValidationConstants.StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Post content
        /// </summary>
        [Required(ErrorMessage = ValidationConstants.RequireErrorMessage)]
        [StringLength(ValidationConstants.ContentMaxLength,
            MinimumLength = ValidationConstants.ContentMinLength,
            ErrorMessage = ValidationConstants.StringLengthErrorMessage)]
        public string Content { get; set; } = string.Empty;
    }
}
