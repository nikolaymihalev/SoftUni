using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models;

public class Writer
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    public string? Pseudonym { get; set; }
    public virtual ICollection<Song> Songs { get; set; }
}