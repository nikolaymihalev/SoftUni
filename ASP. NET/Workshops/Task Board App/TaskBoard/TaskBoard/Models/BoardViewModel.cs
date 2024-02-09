using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;
using Task = TaskBoard.Data.Models.Task;

namespace TaskBoard.Models
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.Board.NameMaxLength, MinimumLength = DataConstants.Board.NameMinLength)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}
