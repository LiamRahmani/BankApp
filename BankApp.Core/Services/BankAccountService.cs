using BankApp.Core.Interfaces;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Core.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepo _repo;

        public BankAccountService(IBankAccountRepo repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponse<List<GetBankAccountsResponseDto>>> AddNewBankAccount(BankAccountCreateDto bankAccount, int customerId)
        {
            return await _repo.AddNewBankAccount(bankAccount, customerId);
        }

        public async Task<ServiceResponse<List<GetBankAccountsResponseDto>>> GetBankAccountsByCustomerId(int customerId)
        {
            return await _repo.GetBankAccountsByCustomerId(customerId);
        }

        public async Task<ServiceResponse<GetBankAccountsResponseDto>> TransferLoan(LoanDto loanDto)
        {
            return await _repo.TransferLoan(loanDto);
        }

        public async Task<ServiceResponse<GetBankAccountsResponseDto>> UpdateBankAccountBalance(TransferMoneyDto transferMoneyDto, int customerId)
        {
            return await _repo.UpdateBankAccountBalance(transferMoneyDto, customerId);
        }
    }
}
