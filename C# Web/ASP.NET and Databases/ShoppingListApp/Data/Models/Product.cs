using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Data.Models
{
    [Comment("Shopping List Product")]
	public class Product
	{
        [Comment("Product Id")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of product")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<ProductNote> ProductNotes { get; set; } = new List<ProductNote>();
    }
}
