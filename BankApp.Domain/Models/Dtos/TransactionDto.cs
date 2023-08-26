using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int bankAccountId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
