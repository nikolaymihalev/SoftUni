namespace Homies.Models
{
    public class EventInfoViewModel
    {
        public EventInfoViewModel(
            int id,
            string name,
            string start,
            string type,
            string organiser)
        {
            Id = id;
            Name = name;
            Start = start;
            Type = type;
            Organiser = organiser;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Start { get; set; }
        public string Type { get; set; }
        public string Organiser { get; set; }
    }
}
