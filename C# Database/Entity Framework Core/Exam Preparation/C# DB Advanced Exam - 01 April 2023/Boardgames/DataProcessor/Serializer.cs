namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System.Net.WebSockets;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            StringBuilder sb = new();

            var creators = context.Creators
                .Where(c => c.Boardgames.Any())
                .Select(c => new ExportCreatorDTO
                {
                    BoardgamesCount = c.Boardgames.Count(),
                    CreatorName = c.FirstName + " " + c.LastName,
                    Boardgames = c.Boardgames.Select(b => new ExportBoardgameDTO
                    {
                        Name = b.Name,
                        YearPublished = b.YearPublished
                    })
                    .OrderBy(b => b.Name)
                    .ToArray()
                })
                .OrderByDescending(c => c.BoardgamesCount)
                    .ThenBy(c => c.CreatorName)
                .ToArray();

            XmlSerializer serialzier = 
                new XmlSerializer(typeof(ExportCreatorDTO[]), new XmlRootAttribute("Creators"));

            XmlSerializerNamespaces xmlns = new();
            xmlns.Add(string.Empty, string.Empty);

            using (StringWriter sw = new(sb))
            {
                serialzier.Serialize(sw, creators, xmlns);
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(s => s.BoardgamesSellers.Any(bgs => bgs.Boardgame.YearPublished >= year
                && bgs.Boardgame.Rating <= rating))
                .Select(s => new
                {
                    Name = s.Name,
                    Website = s.Website,
                    Boardgames = s.BoardgamesSellers
                    .Select(bgs => bgs.Boardgame)
                    .Where(b => b.YearPublished >= year && b.Rating <= rating)
                    .Select(b => new
                    {
                        Name = b.Name,
                        Rating = b.Rating,
                        Mechanics = b.Mechanics,
                        Category = b.CategoryType.ToString()
                    })
                    .OrderByDescending(b => b.Rating)
                        .ThenBy(b => b.Name)
                    .ToArray()
                })
                .OrderByDescending(s => s.Boardgames.Count())
                    .ThenBy(s => s.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }
    }
}