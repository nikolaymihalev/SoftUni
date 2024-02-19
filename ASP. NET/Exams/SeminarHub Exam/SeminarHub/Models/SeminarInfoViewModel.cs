namespace SeminarHub.Models
{
    /// <summary>
    /// View model for seminar entity from database
    /// </summary>
    public class SeminarInfoViewModel
    {
        public SeminarInfoViewModel(
            int id,
            string topic,
            string lecturer,
            string category,
            string dateAndTime,
            string organizer)
        {
            Id = id;
            Topic = topic;
            Lecturer = lecturer;
            Category = category;
            DateAndTime = dateAndTime;
            Organizer = organizer;
        }

        /// <summary>
        /// Seminar's identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Seminar's topic
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Seminar's lecturer
        /// </summary>
        public string Lecturer { get; set; }

        /// <summary>
        /// Seminar's category name
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Seminar's date and time
        /// </summary>
        public string DateAndTime { get; set; }

        /// <summary>
        /// Seminar's organizer username
        /// </summary>
        public string Organizer { get; set; }
    }
}
