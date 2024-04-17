using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using static GameZone.Data.Constants.DataConstants;

namespace GameZone.Data.DataModels
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(GameTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(GameDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        public string PublisherId { get; set; } = string.Empty;

        [ForeignKey(nameof(PublisherId))]
        public IdentityUser Publisher { get; set; } = null!;

        [Required]
        public DateTime ReleasedOn { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; } = null!;

        public List<GamerGame> GamersGames { get; set; } = new List<GamerGame>();
    }
}

