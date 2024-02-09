using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Data.Models;

namespace TaskBoard.Data.Configuration
{
    public class BoardConfiguraion : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasData(new Board[]
            {
                ConfigurationHelper.InProgressBoard,
                ConfigurationHelper.DoneBoard,
                ConfigurationHelper.OpenBoard,
            });
        }
    }
}
