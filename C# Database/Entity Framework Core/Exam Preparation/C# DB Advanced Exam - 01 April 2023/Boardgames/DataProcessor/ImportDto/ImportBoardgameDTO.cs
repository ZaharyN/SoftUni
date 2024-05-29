using Boardgames.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static Boardgames.Constraints.Constants;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Boardgame")]
    public class ImportBoardgameDTO
    {
        [XmlElement("Name")]
        [MinLength(BoardgameNameMinLength)]
        [MaxLength(BoardgameNameMaxLength)]
        [Required]
        public string Name { get; set; }

        [XmlElement("Rating")]
        [Required]
        [Range(BoardgameMinRating, BoardgameMaxRating)]
        public double Rating { get; set; }

        [XmlElement("YearPublished")]
        [Required]
        [Range(BoardgameStartYear, BoardgameEndYear)]
        public int YearPublished { get; set; }

        [XmlElement("CategoryType")]
        [Required]
        public int CategoryType { get; set; }

        [XmlElement("Mechanics")]
        [Required]
        public string Mechanics { get; set; }
    }

}
