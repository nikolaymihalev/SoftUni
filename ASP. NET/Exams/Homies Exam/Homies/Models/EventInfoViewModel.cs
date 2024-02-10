using Homies.Data.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class EventInfoViewModel
    {
        public EventInfoViewModel(int id, string name, DateTime startingDate, string type, string organiser)
        {
            Id = id;
            Name = name;
            Start = startingDate.ToString(ValidationConstants.DataFormat);
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
