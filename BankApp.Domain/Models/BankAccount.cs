using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Balance { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public List<Transactions>? Transactions { get; set; }
    }
}
