using Boardgames.Constraints;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace Boardgames.Data.Models
{
    public class Creator
    {
        public Creator()
        {
            this.Boardgames = new List<Boardgame>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constants.CreatorFirstNameMaxLength)]
        public string FirstName  { get; set; }

        [Required]
        [MaxLength(Constants.CreatorLastNameMaxLength)]
        public string LastName { get; set; }

        public ICollection<Boardgame> Boardgames { get; set; }
    }
}