using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace SeminarHub.Data.Models
{
    [Comment("Seminar table")]
    public class Seminar
    {
        [Key]
        [Comment("Seminar's identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.SeminarTopicMaxLength)]
        [Comment("Seminar's topic")]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.SeminarLecturerMaxLength)]
        [Comment("Seminar's lecturer")]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.SeminarDetailsMaxLength)]
        [Comment("Seminar's details")]
        public string Details { get; set; } = string.Empty;

        [Required]
        [Comment("Seminar's organizer identifier")]
        public string OrganizerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [Comment("Seminar's date and time")]
        public DateTime DateAndTime { get; set; }

        [Comment("Seminar's duration")]
        public int Duration { get; set; }

        [Required]
        [Comment("Seminar's category identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public IList<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }
}