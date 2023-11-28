using Boardgames.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.Data.Models
{
    public class Seller
    {
        public Seller()
        {
            this.BoardgamesSellers = new List<BoardgameSeller>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constants.SellerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(Constants.SellerAddressMaxLength)]
        public string Address  { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Website  { get; set; }

        public ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
    }
}
