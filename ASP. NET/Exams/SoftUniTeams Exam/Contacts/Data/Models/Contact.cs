using Contacts.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Data.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ContactFirstNameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.ContactLastNameMaxLength)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.EmailMaxLength)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.ContactPhoneMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Website { get; set; } = string.Empty;

        public IList<ApplicationUserContact> ApplicationUsersContacts { get; set; } = new List<ApplicationUserContact>();
    }
}
