using EventMe.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMe.Core.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Име на събитие")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = UserMessageConstants.StringLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Начало на събитие")]
        public DateTime Start { get; set; }
        
        [Required(ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Край на събитие")]
        public DateTime End { get; set; }

        [Required(ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Място на провеждане")]
        public string StreetAddress { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = UserMessageConstants.Required)]
        [Display(Name = "Град")]
        public int TownId { get; set; }
    }
}
