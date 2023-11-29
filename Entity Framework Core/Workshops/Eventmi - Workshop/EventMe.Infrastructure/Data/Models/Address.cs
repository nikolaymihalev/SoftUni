using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventMe.Infrastructure.Data.Models
{
    /// <summary>
    /// Място на провеждане 
    /// </summary>
    [Comment("Място на провеждане")]
    public class Address
    {
        /// <summary>
        /// Индентификатор на мястото 
        /// </summary>
        [Comment("Индентификатор на мястото")]
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Индентификатор на града 
        /// </summary>
        [Comment("Индентификатор на града")]
        [Required]
        public int TownId { get; set; }

        /// <summary>
        /// Адрес на мястото 
        /// </summary>
        [Comment("Адрес на мястото ")]
        [Required]
        [MaxLength(100)]
        public string StreetAddress { get; set; } = null!;

        /// <summary>
        /// Града 
        /// </summary>
        [Comment("Града")]
        [Required]
        [ForeignKey(nameof(TownId))]
        public Town Town { get; set; } = null!;
    }
}
