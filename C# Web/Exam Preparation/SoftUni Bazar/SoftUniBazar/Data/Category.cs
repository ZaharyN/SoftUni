using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Constants.DataConstants;

namespace SoftUniBazar.Data
{
	public class Category
	{
		[Key]
        public int Id { get; set; }

		[Required]
		[StringLength(CategoryNameMaxLength)]
		public string Name { get; set; } = string.Empty;

		public List<Ad> Ads { get; set; } = new List<Ad>();
    }
}