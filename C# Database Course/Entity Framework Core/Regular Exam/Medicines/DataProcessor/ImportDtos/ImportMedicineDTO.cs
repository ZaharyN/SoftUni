using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImportMedicineDTO
    {
        [XmlAttribute("category")]
        [Required]
        [Range(0,4)]
        public int Category { get; set; }

        [XmlElement("Name")]
        [Required]
        [MaxLength(150)]
        [MinLength(3)]
        public string Name { get; set; }

        [XmlElement("Price")]
        [Required]
        [Range(0.01,1000.00)]
        public decimal Price { get; set; }

        [XmlElement("ProductionDate")]
        [Required]
        public string ProductionDate { get; set; }

        [XmlElement("ExpiryDate")]
        [Required]
        public string ExpiryDate { get; set; }

        [XmlElement("Producer")]
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Producer { get; set; }
    }
}
