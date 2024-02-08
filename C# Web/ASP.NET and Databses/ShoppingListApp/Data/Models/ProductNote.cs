using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Data.Models
{
    [Comment("Product Note")]
	public class ProductNote
	{
        [Key]
        [Comment("Note Identifier")]
        public int Id { get; set; }

        [Comment("Note Content")]
        [Required]
        [StringLength(150)]
        public string Coantent { get; set; } = string.Empty;

        [Comment("Product Identifier")]
        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}