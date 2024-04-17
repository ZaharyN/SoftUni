using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Data.DataModels
{
    public class GamerGame
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;

        [Required]
        public string GamerId { get; set; } = string.Empty;

        [Required]
        public IdentityUser Gamer { get; set; } = null!;
    }
}