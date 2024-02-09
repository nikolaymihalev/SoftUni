using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Configuration;
using TaskBoard.Data.Models;
using Task =  TaskBoard.Data.Models.Task;

namespace TaskBoard.Data
{
    public class TaskBoardAppDbContext : IdentityDbContext
    {
        public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguraion());
            builder.ApplyConfiguration(new BoardConfiguraion());
            builder.ApplyConfiguration(new TaskConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; }
    }
}
