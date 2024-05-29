namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new();

            StringReader reader = new StringReader(xmlString);

            XmlSerializer serializer =
                new XmlSerializer(typeof(ImportCreatorDTO[]), new XmlRootAttribute("Creators"));

            ImportCreatorDTO[] creatorsDTO = serializer.Deserialize(reader) as ImportCreatorDTO[];

            List<Creator> creators = new List<Creator>();

            foreach (ImportCreatorDTO creatorDTO in creatorsDTO)
            {
                if (!IsValid(creatorDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Creator creator = new Creator()
                {
                    FirstName = creatorDTO.FirstName,
                    LastName = creatorDTO.LastName
                };

                foreach (ImportBoardgameDTO boardgameDTO in creatorDTO.Boardgames)
                {
                    if (!IsValid(boardgameDTO))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame boardgame = new Boardgame()
                    {
                        Name = boardgameDTO.Name,
                        Rating = boardgameDTO.Rating,
                        YearPublished = boardgameDTO.YearPublished,
                        CategoryType = (CategoryType)boardgameDTO.CategoryType,
                        Mechanics = boardgameDTO.Mechanics
                    };
                    creator.Boardgames.Add(boardgame);
                }
                creators.Add(creator);
                sb.AppendLine(string
                    .Format(SuccessfullyImportedCreator, creator.FirstName, creator.LastName, creator.Boardgames.Count));
            }
            context.AddRange(creators);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new();

            ImportSellerDTO[] sellersDTO = JsonConvert.DeserializeObject<ImportSellerDTO[]>(jsonString);

            List<Seller> sellers = new List<Seller>();

            foreach (var sellerDTO in sellersDTO)
            {
                if (!IsValid(sellerDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Seller seller = new()
                {
                    Name = sellerDTO.Name,
                    Address = sellerDTO.Address,
                    Country = sellerDTO.Country,
                    Website = sellerDTO.Website,
                };

                int[] boardgamesId = context.Boardgames
                    .Select(b => b.Id)
                    .ToArray();

                foreach (int boardgameId in sellerDTO.Boardgames.Distinct())
                {
                    if (!boardgamesId.Contains(boardgameId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    seller.BoardgamesSellers.Add(new BoardgameSeller
                    {
                        BoardgameId = boardgameId
                    });
                }
                sellers.Add(seller);

                sb.AppendLine(string
                    .Format(SuccessfullyImportedSeller,seller.Name,seller.BoardgamesSellers.Count()));
            }
            context.AddRange(sellers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
