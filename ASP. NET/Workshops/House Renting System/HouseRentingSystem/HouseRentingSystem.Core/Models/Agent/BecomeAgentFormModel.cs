using HouseRentingSystem.Core.Constants;
using HouseRentingSystem.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Core.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [StringLength(DataConstants.AgentPhoneMaxLength, 
            MinimumLength = DataConstants.AgentPhoneMinLength,
            ErrorMessage = MessageConstants.LengthMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
