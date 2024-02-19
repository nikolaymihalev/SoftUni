namespace SeminarHub.Models
{
    /// <summary>
    /// View model for seminar's details
    /// </summary>
    public class SeminarDetailsViewModel
    {
        public SeminarDetailsViewModel(
            int id,
            string topic,
            string lecturer,
            string details,
            int duration,
            string dateAndTime,
            string category,
            string orginzer)
        {
            Id = id;
            Topic = topic;
            Lecturer = lecturer;
            Details = details;
            Duration = duration;
            DateAndTime = dateAndTime;
            Category = category;
            Organizer = orginzer;
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
        /// Seminar's details
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Seminar's duration
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Seminar's date and time
        /// </summary>
        public string DateAndTime { get; set; }

        /// <summary>
        /// Seminar's category name
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Seminar's organizer username
        /// </summary>
        public string Organizer { get; set; }
    }
}