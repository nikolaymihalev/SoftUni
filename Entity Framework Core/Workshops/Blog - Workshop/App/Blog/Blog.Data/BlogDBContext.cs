using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class BlogDBContext : IdentityDbContext<IdentityUser>
    {
        public BlogDBContext()
        {
            
        }
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Blog;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");
            }
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Article>().Property(a => a.CreatedOn).HasDefaultValue(DateTime.Now);
        }
    }
}
