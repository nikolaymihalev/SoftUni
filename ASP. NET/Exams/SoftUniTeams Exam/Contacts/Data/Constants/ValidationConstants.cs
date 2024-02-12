namespace Contacts.Data.Constants
{
    public static class ValidationConstants
    {
        public const int UserNameMaxLength = 20;
        public const int UserNameMinLength = 5; 
        
        public const int EmailMaxLength = 60;
        public const int EmailMinLength = 10;
        
        public const int ContactFirstNameMaxLength = 50;
        public const int ContactFirstNameMinLength = 2;
        
        public const int ContactLastNameMaxLength = 50;
        public const int ContactLastNameMinLength = 5;
        
        public const int ContactPhoneMaxLength = 13;
        public const int ContactPhoneMinLength = 10;
        
        public const int ContactPasswordMaxLength = 20;
        public const int ContactPasswordMinLength = 5;

        public const string ContactPhoneRegularExpression = @"\+\d{3}\-\d{3}\-\d{2}\-\d{2}\-\d{2}|0\d{9}";
        public const string ContactWebsiteRegularExpression = @"www\.\w{1,}\.bg";
    }
}
