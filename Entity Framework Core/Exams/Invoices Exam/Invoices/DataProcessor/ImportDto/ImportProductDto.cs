using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.DataProcessor.ImportDto
{
    public class ImportProductDto
    {
        [Required]
        [MaxLength(30)]
        [MinLength(9)]
        public string Name { get; set; }

        [Required]
        [Range(5.00,1000.00)]
        public double Price { get; set; }

        [Required]
        [Range(0,4)]
        public int CategoryType { get; set; }
        public int[] Clients { get; set; }
    }
}
