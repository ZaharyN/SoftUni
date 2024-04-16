using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Constants.DataConstants;
using static SoftUniBazar.Models.Constants.MessageConstants;

namespace SoftUniBazar.Models.AdViewModels
{
	public class AdEditFormViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
		[StringLength(AdNameMaxLength,
			MinimumLength = AdNameMinLength,
			ErrorMessage = LengthMessage)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(AdDescriptionMaxLength,
			MinimumLength = AdDescriptionMinLength,
			ErrorMessage = LengthMessage)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public string ImageUrl { get; set; } = string.Empty;

		[Required(ErrorMessage = RequiredMessage)]
		public decimal Price { get; set; }

		[Required]
		public int CategoryId { get; set; }

		public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
	}
}
