namespace HouseRentingSystem.Core.Constants
{
    public static class MessageConstants
    {
        public const string RequiredMessage = "The field {0} is required!";
        public const string LengthMessage = "The field {0} must be between {2} and {1} characters long!";
        public const string PhoneExists = "Phone number already exists. Enter another one!";
        public const string HasRents = "You should have no rents to become an agent";
        public const string PricePositiveNumber = "Price per month must be a positive number and less than {2}!";
    }
}