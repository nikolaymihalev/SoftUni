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
                    .AppendLine($"-ReleaseDate: {album.ReleaseDate}")
                    .AppendLine($"-ProducerName: {album.ProducerName}")
                    .AppendLine($"-Songs:");
                if (album.AlbumSongs.Any())
                {
                    int count = 1;
                    foreach (var song in album.AlbumSongs)
                    {
                        sb.AppendLine($"---#{count++}")
                            .AppendLine($"---SongName: {song.SongName}")
                            .AppendLine($"---Price: {song.SongPrice}")
                            .AppendLine($"---Writer: {song.SongWriterName}");
                    }
                }

                sb.AppendLine($"AlbumPrice: {album.TotalAlbumPrice:f2}");
            }
            return sb.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            throw new NotImplementedException();
        }
    }
}
