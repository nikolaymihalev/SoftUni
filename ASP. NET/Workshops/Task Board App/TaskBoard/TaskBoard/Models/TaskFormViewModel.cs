using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;

namespace TaskBoard.Models
{
    public class TaskFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.Task.TitleMaxLength, 
            MinimumLength = DataConstants.Task.TitleMinLength,
            ErrorMessage = "The field {0} must be between {2} and {1} characters long")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(DataConstants.Task.DescriptionMaxLength,
            MinimumLength = DataConstants.Task.DescriptionMinLength,
            ErrorMessage = "The field {0} must be between {2} and {1} characters long")]
        public string Description { get; set; } = string.Empty;

        public int? BoardId { get; set; }

        public IEnumerable<TaskBoardViewModel> Boards { get; set; } = new List<TaskBoardViewModel>();
    }
}
