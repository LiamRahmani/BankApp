using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Interfaces
{
    public interface IBankAccountService
    {
        Task<ServiceResponse<List<GetBankAccountsResponseDto>>> GetBankAccountsByCustomerId(int customerId);
        Task<ServiceResponse<List<GetBankAccountsResponseDto>>> AddNewBankAccount(BankAccountCreateDto bankAccount, int customerId);
        Task<ServiceResponse<GetBankAccountsResponseDto>> UpdateBankAccountBalance(TransferMoneyDto transferMoneyDto, int customerId);
        Task<ServiceResponse<GetBankAccountsResponseDto>> TransferLoan(LoanDto loanDto);
    }
}
