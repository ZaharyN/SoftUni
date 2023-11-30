using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Invoices.Data.Models
{
    public class Client
    {
        public Client()
        {
            this.Invoices = new List<Invoice>();
            this.Addresses = new List<Address>();
            this.ProductsClients = new List<ProductClient>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string NumberVat  { get; set; }

        public virtual ICollection<Invoice> Invoices  { get; set; }
        public virtual ICollection<Address> Addresses  { get; set; }
        public virtual ICollection<ProductClient> ProductsClients { get; set; }
    }
}