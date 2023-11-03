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
            
            throw new NotImplementedException();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            throw new NotImplementedException();
        }
    }
}
