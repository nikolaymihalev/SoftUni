using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.DataProcessor.ImportDto
{
    public class ImportInvoiceDto
    {
        [Required]
        [Range(1_000_000_000, 1_500_000_000)]
        public int Number { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        [Range(0,2)]
        public int CurrencyType { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}
