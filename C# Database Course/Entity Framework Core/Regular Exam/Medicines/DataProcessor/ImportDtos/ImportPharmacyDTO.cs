using Medicines.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmacyDTO
    {
        [XmlAttribute("non-stop")]
        [Required]
        public string IsNonStop { get; set; }

        [XmlElement("Name")]
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }

        [XmlElement("PhoneNumber")]
        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        [RegularExpression(@"^\(\d{3}\)\s\d{3}-\d{4}$")]
        public string PhoneNumber { get; set; }

        [XmlArray("Medicines")]
        public ImportMedicineDTO[] Medicines { get; set; }
    }
}
