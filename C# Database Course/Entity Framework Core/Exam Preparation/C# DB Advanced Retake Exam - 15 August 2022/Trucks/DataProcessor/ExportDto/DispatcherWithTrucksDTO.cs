using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Despatcher")]
    public class DispatcherWithTrucksDTO
    {
        [XmlAttribute("TrucksCount")]
        public int TrucksCount { get; set; }

        [XmlElement("DespatcherName")]
        [MinLength(2)]
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }

        [XmlArray("Trucks")]
        public ExportTruckDTO[] Trucks { get; set; }

    }
}
