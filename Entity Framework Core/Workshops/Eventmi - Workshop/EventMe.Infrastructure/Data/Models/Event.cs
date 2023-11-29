using EventMe.Infrastructure.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventMe.Infrastructure.Data.Models
{
    /// <summary>
    /// Събитие
    /// </summary>
    [Comment("Събитие")]
    public class Event:IDeletable
    {
        /// <summary>
        /// Идентификатор на събитието
        /// </summary>
        [Comment("Идентификатор на събитието")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Име на събитието
        /// </summary>
        [Comment("Име на събитието")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        
        /// <summary>
        /// Начало на събитието
        /// </summary>
        [Comment("Начало на събитието")]
        [Required]
        public DateTime Start { get; set; } 
        
        /// <summary>
        /// Край на събитието
        /// </summary>
        [Comment("Край на събитието")]
        [Required]
        public DateTime End { get; set; }

        /// <summary>
        /// Индефикатор на мястото
        /// </summary>
        [Comment("Индефикатор на мястото")]
        [Required]
        public int PlaceId { get; set; }

        /// <summary>
        /// Събитието е активно
        /// </summary>
        [Comment("Събитието е активно")]
        [Required]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Дата на изтриване
        /// </summary>
        [Comment("Дата на изтриване")]
        public DateTime? DeletedOn { get; set; }

        /// <summary>
        /// Място на провеждане на събитие
        /// </summary>
        [Comment("Място на провеждане на събитие")]
        [ForeignKey(nameof(PlaceId))]
        public Address Place { get; set; } = null!;
    }
}
