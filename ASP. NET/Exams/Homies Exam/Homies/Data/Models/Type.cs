﻿using Homies.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.TypeNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IList<Event> Events { get; set; } = new List<Event>();
    }
}
