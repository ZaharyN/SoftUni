namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml.Linq;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();
            
            DbInitializer.ResetDatabase(context);

            //Console.WriteLine(ExportAlbumsInfo(context, 9));
            Console.WriteLine(ExportSongsAboveDuration(context, 4));

        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumsInfo = context.Producers
                .Include(p => p.Albums)
                    .ThenInclude(p => p.Songs)
                        .ThenInclude(p => p.Writer)
                .FirstOrDefault(p => p.Id == producerId)
                .Albums.Select(a => new
                {
                    a.Name,
                    ProducerName = a.Producer.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    Songs = a.Songs.Select(s => new
                    {
                        s.Name,
                        s.Price,
                        SongWriterName = s.Writer.Name
                    })
                    .OrderByDescending(s => s.Name)
                    .ThenBy(sw => sw.SongWriterName)
                    .ToList(),
                    AlbumPrice = a.Price
                })
                .OrderByDescending(a => a.AlbumPrice)
                .ToList();


            StringBuilder sb = new();

            
            foreach (var album in albumsInfo)
            {
                sb.AppendLine($"-AlbumName: {album.Name}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine($"-Songs:");

                int counter = 1;
                foreach (var song in album.Songs)
                {
                    sb.AppendLine($"---#{counter++}");
                    sb.AppendLine($"---SongName: {song.Name}");
                    sb.AppendLine($"---Price: {song.Price:f2}");
                    sb.AppendLine($"---Writer: {song.SongWriterName}");
                }
                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:f2}");
            }
            
            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .Include(s => s.SongPerformers)
                    .ThenInclude(s => s.Performer)
                .Include(s => s.Writer)
                .Include(s => s.Album)
                .AsEnumerable()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    s.Name,
                    PerformersFullName = s.SongPerformers
                        .Select(sp => new
                        {
                            PerformerFullName = $"{sp.Performer.FirstName} {sp.Performer.LastName}"
                        })
                        .OrderBy(p => p.PerformerFullName)
                        .ToList(),
                    WriterName = s.Writer.Name,
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c")
                })
                .OrderBy(s => s.Name)
                .ThenBy(sw => sw.WriterName)
                .ToList();

            StringBuilder sb = new();

            int counter = 1;
            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{counter++}");
                sb.AppendLine($"---SongName: {song.Name}");
                sb.AppendLine($"---Writer: {song.WriterName}");

                if(song.PerformersFullName.Any())
                {
                    foreach (var performer in song.PerformersFullName)
                    {
                        sb.AppendLine($"---Performer: {performer.PerformerFullName}");
                    }
                }

                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
