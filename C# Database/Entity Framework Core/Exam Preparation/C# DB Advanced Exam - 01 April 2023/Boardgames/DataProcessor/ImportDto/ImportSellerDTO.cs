using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boardgames.Constraints.Constants;

namespace Boardgames.DataProcessor.ImportDto
{
    public class ImportSellerDTO
    {
        [JsonProperty("Name")]
        [Required]
        [MinLength(SellerNameMinLength)]
        [MaxLength(SellerNameMaxLength)]
        public string Name { get; set; }

        [JsonProperty("Address")]
        [Required]
        [MinLength(SellerAddressMinLength)]
        [MaxLength(SellerAddressMaxLength)]
        public string Address { get; set; }

        [JsonProperty("Country")]
        [Required]
        public string Country { get; set; }

        [JsonProperty("Website")]
        [Required]
        [RegularExpression(SellerWebsiteRegEx)]
        public string Website { get; set; }

        [JsonProperty("Boardgames")]
        public int[] Boardgames { get; set; }
    }
}
