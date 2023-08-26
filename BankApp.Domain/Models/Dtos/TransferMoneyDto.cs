using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models.Dtos
{
    public class TransferMoneyDto
    {
        public int BankAccountFromId { get; set; }
        public int BankAccountToId { get;set; }
        public int Amount { get; set; }
    }
}
