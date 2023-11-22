using EventMe.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventMe.Infrastructure.Data.Configuration
{
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        readonly string[] towns = new string[] {"София","Пловдив","Варна","Бургас" };

        public void Configure(EntityTypeBuilder<Town> builder)
        {
            List<Town> entities = new();
            int id = 1;

            foreach (var item in towns)
            {
                entities.Add(new Town {
                    Id = id++,
                    Name = item
                });
            }

            builder.HasData(entities);
        }
    }
}
