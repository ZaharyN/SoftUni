using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Trucks.Data.Models
{
    public class Despatcher
    {
        public Despatcher()
        {
            this.Trucks = new HashSet<Truck>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        public string Position { get; set; }

        public ICollection<Truck> Trucks { get; set; }
    }
}

//•	Id – integer, Primary Key
//•	Name – text with length [2, 40] (required)
//•	Position – text
//•	Trucks – collection of type Truck
