using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using static SoftUniBazar.Data.Constants.DataConstants;

namespace SoftUniBazar.Data
{
	public class Ad
	{
		[Key]
        public int Id { get; set; }

		[Required]
		[StringLength(AdNameMaxLength)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[StringLength(AdDescriptionMaxLength)]
		public string Description { get; set; } = string.Empty;

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Required]
		public string OwnerId { get; set; } = string.Empty;

		[Required]
		[ForeignKey(nameof(OwnerId))]
		public IdentityUser Owner { get; set; } = null!;

		[Required]
		public string ImageUrl { get; set; } = string.Empty;

		[Required]
        public DateTime CreatedOn { get; set; }

		[Required]
        public int CategoryId { get; set; }

		[Required]
		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } = null!;

		public List<AdBuyer> AdsBuyers { get; set; } = new List<AdBuyer>();
    }
}

