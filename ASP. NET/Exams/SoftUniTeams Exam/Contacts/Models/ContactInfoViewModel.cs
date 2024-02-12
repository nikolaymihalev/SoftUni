namespace Contacts.Models
{
    public class ContactInfoViewModel
    {
        public ContactInfoViewModel(
            int id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string address,
            string website)
        {
            ContactId = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Website = website;
        }

        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
    }
}
