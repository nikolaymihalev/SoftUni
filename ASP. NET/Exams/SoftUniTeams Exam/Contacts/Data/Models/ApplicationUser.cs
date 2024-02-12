using Contacts.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Data.Models
{
    public class ApplicationUser
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.EmailMaxLength)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public IList<ApplicationUserContact> ApplicationUsersContacts { get; set; } = new List<ApplicationUserContact>();

    }
}
