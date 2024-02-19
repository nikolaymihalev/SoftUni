namespace SeminarHub.Data.Constants
{
    /// <summary>
    /// Class with constants for validation
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Constant for formatting dates
        /// </summary>
        public const string DateFormat = "dd/MM/yyyy HH:mm";

        /// <summary>
        /// Constant for seminar's topic max length value
        /// </summary>
        public const int SeminarTopicMaxLength = 100;

        /// <summary>
        /// Constant for seminar's topic min length value
        /// </summary>
        public const int SeminarTopicMinLength = 3;

        /// <summary>
        /// Constant for seminar's lecturer max length value
        /// </summary>
        public const int SeminarLecturerMaxLength = 60;

        /// <summary>
        /// Constant for seminar's lecturer min length value
        /// </summary>
        public const int SeminarLecturerMinLength = 5;

        /// <summary>
        /// Constant for seminar's details max length value
        /// </summary>
        public const int SeminarDetailsMaxLength = 500;

        /// <summary>
        /// Constant for seminar's details min length value
        /// </summary>
        public const int SeminarDetailsMinLength = 10;

        /// <summary>
        /// Constant for seminar's duration max length value
        /// </summary>
        public const int SeminarDurationMaxValue = 180;

        /// <summary>
        /// Constant for seminar's duration min length value
        /// </summary>
        public const int SeminarDurationMinValue = 30;

        /// <summary>
        /// Constant for category's name max length value
        /// </summary>
        public const int CategoryNameMaxLength = 50;

        /// <summary>
        /// Constant for category's name min length value
        /// </summary>
        public const int CategoryNameMinLength = 3;
    }
}
