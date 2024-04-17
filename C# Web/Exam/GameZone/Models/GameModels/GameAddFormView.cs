using GameZone.Data.DataModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameZone.Data.Constants.DataConstants;
using static GameZone.Models.Constants.MessageConstants;

namespace GameZone.Models.GameModels
{
    public class GameAddFormView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(GameTitleMaxLength,
            MinimumLength = GameTitleMinLength,
            ErrorMessage = LengthMessage)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(GameDescriptionMaxLength,
            MinimumLength = GameDescriptionMinLength,
            ErrorMessage = LengthMessage)]
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        public string ReleasedOn { get; set; } = string.Empty;

        [Required]
        public int GenreId { get; set; }

        public List<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();

    }
}
