using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models
{
    [Comment("Seminar participant mapping table")]
    public class SeminarParticipant
    {
        [Required]
        [Comment("Seminar's identifier")]
        public int SeminarId { get; set; }

        [ForeignKey(nameof(SeminarId))]
        public Seminar Seminar { get; set; }

        [Required]
        [Comment("Participant's identifier")]
        public string ParticipantId { get; set; }

        [ForeignKey(nameof(ParticipantId))]
        public IdentityUser Participant { get; set; }

    }
}