using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Client")]
    public class ExportClientDTO
    {
        [XmlAttribute("InvoicesCount")]
        public int InvoicesCount { get; set; }

        [XmlElement("ClientName")]
        [Required]
        public string Name { get; set; } = null!;

        [XmlElement("VatNumber")]
        [Required]
        public string NumberVat { get; set; }

        [XmlArray("Invoices")]
        public ExportInvoiceDTO[] Invoices { get; set; }
    }
}
