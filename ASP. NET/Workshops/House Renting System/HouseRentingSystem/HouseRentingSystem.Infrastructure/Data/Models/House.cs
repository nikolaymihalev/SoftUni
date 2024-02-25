using HouseRentingSystem.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Comment("House to rent")]
    public class House
    {
        [Key]
        [Comment("House identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.HouseTitleMaxLength)]
        [Comment("House title")]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(DataConstants.HouseAddressMaxLength)]
        [Comment("House address")]
        public string Address { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(DataConstants.HouseDescriptionMaxLength)]
        [Comment("House description")]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Comment("House image url")]
        public string ImageUrl { get; set; } = string.Empty;
        
        [Required]
        //[Range(typeof(decimal), DataConstants.HouseRentingPriceMin,DataConstants.HouseRentingPriceMax, ConvertValueInInvariantCulture = true)]
        [Comment("House price per month")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerMonth { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; } 
        
        [Required]
        [Comment("Agent identifier")]
        public int AgentId { get; set; }
        
        [Comment("User id of the renterer")]
        public string? RenterId { get; set; }

        public Category Category { get; set; } = null!;
        
        public Agent Agent { get; set; } = null!;
    }
}