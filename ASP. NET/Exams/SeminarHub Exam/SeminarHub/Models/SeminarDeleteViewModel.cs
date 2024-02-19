namespace SeminarHub.Models
{
    /// <summary>
    /// View model for deleting seminar
    /// </summary>
    public class SeminarDeleteViewModel
    {
        public SeminarDeleteViewModel(
            int id,
            string topic,
            DateTime dateAndTime,
            string organizer)
        {
            Id = id;
            Topic = topic;
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
        /// Seminar's date and time
        /// </summary>
        public DateTime DateAndTime { get; set; }

        /// <summary>
        /// Seminar's organizer username
        /// </summary>
        public string Organizer { get; set; }
    }
}
