using SoftUniBazar.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IList<Ad> Ads { get; set; } = new List<Ad>();
    }
}