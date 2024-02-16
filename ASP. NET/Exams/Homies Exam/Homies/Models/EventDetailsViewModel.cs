namespace Homies.Models
{
    public class EventDetailsViewModel
    {
        public EventDetailsViewModel(int id,
            string name,
            string description,
            string start,
            string end,
            string organiser,
            string createdOn,
            string type)
        {
            Id = id;
            Name = name;
            Description = description;
            Start = start;
            End = end;
            Organiser = organiser;
            CreatedOn = createdOn;
            Type = type;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Organiser { get; set; }
        public string CreatedOn { get; set; }
        public string Type { get; set; }
    }
}
