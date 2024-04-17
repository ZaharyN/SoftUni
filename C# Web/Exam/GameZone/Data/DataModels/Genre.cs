using System.ComponentModel.DataAnnotations;
using static GameZone.Data.Constants.DataConstants;

namespace GameZone.Data.DataModels
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(GenreNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public List<Game> Games { get; set; } = new List<Game>();
    }
}