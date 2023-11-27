using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Truck")]
    public class ExportTruckDTO
    {
        [Required]
        [XmlElement("RegistrationNumber")]
        [RegularExpression(@"[A-Z]{2}[0-9]{4}[A-Z]{2}$")]
        public string RegistrationNumber { get; set; }

        [Required]
        [XmlElement("Make")]
        public string Make { get; set; }

    }
}
