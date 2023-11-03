using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MusicHub
{
    using System;

    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumInfo = context.Producers
                .Include(x=>x.Albums).ThenInclude(x=>x.Songs).ThenInclude(s=>s.Writer)
                .First(x => x.Id == producerId)
                .Albums.Select(a=>new
                {
                    AlbumName= a.Name,
                    ReleaseDate = a.ReleaseDate,
                    ProducerName = a.Producer.Name,
                    AlbumSongs = a.Songs.Select(s=>new
                    {
                        SongName= s.Name,
                        SongPrice= s.Price,
                        SongWriterName=s.Writer.Name
                    }).OrderByDescending(x=>x.SongName).ThenBy(x=>x.SongWriterName),
                    TotalAlbumPrice=a.Price
                }).OrderByDescending(x=>x.TotalAlbumPrice);
            
            StringBuilder sb = new StringBuilder();

            foreach (var album in albumInfo)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}")
                    .AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}")
                    .AppendLine($"-ProducerName: {album.ProducerName}")
                    .AppendLine($"-Songs:");
                if (album.AlbumSongs.Any())
                {
                    int count = 1;
                    foreach (var song in album.AlbumSongs)
                    {
                        sb.AppendLine($"---#{count++}")
                            .AppendLine($"---SongName: {song.SongName}")
                            .AppendLine($"---Price: {song.SongPrice:f2}")
                            .AppendLine($"---Writer: {song.SongWriterName}");
                    }
                }

                sb.AppendLine($"-AlbumPrice: {album.TotalAlbumPrice:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .Include(s => s.SongsPerformers)
                    .ThenInclude(sp => sp.Performer)
                .Include(s => s.Writer)
                .Include(s => s.Album)
                    .ThenInclude(a => a.Producer)
                .AsEnumerable()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s=> new
                {
                    s.Name,
                    Performers = s.SongsPerformers.Select(sp=>sp.Performer.FirstName+" "+sp.Performer.LastName).ToList(),
                    WriterName = s.Writer.Name,
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c")
                })
                .OrderBy(s=>s.Name)
                .ThenBy(s=>s.WriterName)
                .ToList();
            throw new NotImplementedException();
        }
    }
}
