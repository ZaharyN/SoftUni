using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Medicines.Data.Models
{
    public class Medicine
    {
        public Medicine()
        {
            this.PatientsMedicines = new List<PatientMedicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public DateTime ProductionDate  { get; set; }

        [Required]
        public DateTime ExpiryDate  { get; set; }

        [Required]
        [MaxLength(100)]
        public string Producer { get; set; }

        [Required]
        [ForeignKey(nameof(Pharmacy))]
        public int PharmacyId  { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }

        public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; }
    }
}