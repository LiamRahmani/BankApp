using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;

namespace BankApp.Data.Interfaces
{
    public interface IBankAccountRepo
    {
        Task<ServiceResponse<List<GetBankAccountsResponseDto>>> GetBankAccountsByCustomerId(int customerId);
        Task<ServiceResponse<List<GetBankAccountsResponseDto>>> AddNewBankAccount(BankAccountCreateDto bankAccount, int customerId);
        Task<ServiceResponse<GetBankAccountsResponseDto>> UpdateBankAccountBalance(TransferMoneyDto transferMoneyDto, int customerId);
        Task<ServiceResponse<GetBankAccountsResponseDto>> TransferLoan(LoanDto loanDto);
    }
}
