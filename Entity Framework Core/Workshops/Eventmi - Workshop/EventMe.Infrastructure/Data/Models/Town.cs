using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventMe.Infrastructure.Data.Models
{
    /// <summary>
    /// Град 
    /// </summary>
    [Comment("Град")]
    public class Town
    {
        /// <summary>
        /// Индентификатор на град 
        /// </summary>
        [Comment("Индентификатор на град")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Име на град 
        /// </summary>
        [Comment("Име на град")]
        [Required]
        public string Name { get; set; } = null!;
    }
}
