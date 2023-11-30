using Invoices.Data.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.DataProcessor.ImportDto
{
    public class ImportProductDTO
    {
        [JsonProperty("Name")]
        [Required]
        [MaxLength(30)]
        [MinLength(9)]
        public string Name { get; set; }

        [JsonProperty("Price")]
        [Required]
        [Range(5.00,1000.00)]
        public decimal Price { get; set; }

        [Required]
        [JsonProperty("CategoryType")]
        [Range(0,4)]
        public int CategoryType { get; set; }

        public int[] Clients { get; set; } = null!;
    }
}
