using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models.Dtos
{
    public class LoanDto
    {
        public int CustomerId { get; set; }
        public string LoanDescription { get; set;} = string.Empty;
        public int Amount { get; set; }

    }
}
