using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Data.Models;
using Task = TaskBoard.Data.Models.Task;

namespace TaskBoard.Data.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {    
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedTasks());
        }

        private IEnumerable<Task> SeedTasks() 
        {
            return new Task[]
            {
                new Task()
                {
                    Id = 1,
                    Title = "Improve CSS styles",
                    Description = "Implement better styling for all public pages",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = ConfigurationHelper.TestUser.Id,
                    BoardId = ConfigurationHelper.OpenBoard.Id
                },
                new Task()
                {
                    Id = 2,
                    Title = "Improve CSS",
                    Description = "Implement styling for all public pages",
                    CreatedOn = DateTime.Now.AddDays(-150),
                    OwnerId = ConfigurationHelper.TestUser.Id,
                    BoardId = ConfigurationHelper.OpenBoard.Id
                }
            };
        }
    }
}
