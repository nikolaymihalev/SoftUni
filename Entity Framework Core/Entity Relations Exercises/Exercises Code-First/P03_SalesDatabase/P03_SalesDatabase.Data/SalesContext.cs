using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03_SalesDatabase.P03_SalesDatabase.Data
{
    public class SalesContext:DbContext
    {
        const string ConnectionString = "Server=.;Database=Sales;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false";

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(50).IsUnicode(true);

            modelBuilder.Entity<Customer>().Property(c => c.Name).HasMaxLength(100).IsUnicode(true);
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(80).IsUnicode(false);

            modelBuilder.Entity<Store>().Property(s => s.Name).HasMaxLength(80).IsUnicode(true);
        }
    }
}
