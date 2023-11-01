using Microsoft.EntityFrameworkCore;
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
