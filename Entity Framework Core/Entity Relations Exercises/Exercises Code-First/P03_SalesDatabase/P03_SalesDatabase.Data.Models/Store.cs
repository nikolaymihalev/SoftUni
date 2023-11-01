using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03_SalesDatabase.P03_SalesDatabase.Data.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
