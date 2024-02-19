using Microsoft.EntityFrameworkCore;
using SeminarHub.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Data.Models
{
    [Comment("Category table")]
    public class Category
    {
        [Key]
        [Comment("Category's identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Category's name")]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IList<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}