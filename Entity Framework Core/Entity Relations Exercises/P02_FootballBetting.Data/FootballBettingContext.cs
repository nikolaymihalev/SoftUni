using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        const string ConnectionString = "Server=.;Database=FootballBetting;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false";

        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
