using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Boardgames.Constraints.Constants;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Creator")]
    public class ImportCreatorDTO
    {
        [XmlElement("FirstName")]
        [Required]
        [MinLength(CreatorFirstNameMinLength)]
        [MaxLength(CreatorFirstNameMaxLength)]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        [Required]
        [MinLength(CreatorLastNameMinLength)]
        [MaxLength(CreatorLastNameMaxLength)]
        public string LastName { get; set; }

        [XmlArray("Boardgames")]
        public ImportBoardgameDTO[] Boardgames { get; set; }
    }
}
