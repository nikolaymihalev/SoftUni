using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityAdvancedDemo.Data.IdentityModels
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        [MaxLength(50)]
        public string? BGName { get; set; }
    }
}
