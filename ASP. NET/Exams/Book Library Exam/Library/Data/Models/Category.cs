using Library.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IList<Book> Books { get; set; } = new List<Book>();
    }
}