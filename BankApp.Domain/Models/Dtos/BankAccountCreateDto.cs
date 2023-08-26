using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Models.Dtos
{
    public class BankAccountCreateDto
    {
        public string Type { get; set; } = string.Empty;
        public int Balance { get; set; }
    }
}
