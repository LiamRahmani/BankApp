using AutoMapper;
using BankApp.Data.DataModels;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.Repos
{
    public class BankAccountRepo : IBankAccountRepo
    {
        private readonly BankAppDBContext _db;
        private readonly IMapper _mapper;

        public BankAccountRepo(BankAppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetBankAccountsResponseDto>>> AddNewBankAccount(BankAccountCreateDto newBankAccount, int customerId)
        {
            var response = new ServiceResponse<List<GetBankAccountsResponseDto>>();
            var bankAccount = _mapper.Map<BankAccount>(newBankAccount);
            bankAccount.Customer = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            _db.BankAccounts.Add(bankAccount);
            await _db.SaveChangesAsync();

            response.Data =
                await _db.BankAccounts
                    .Where(ac => ac.CustomerId == customerId)
                    .Select(c => _mapper.Map<GetBankAccountsResponseDto>(c))
                    .ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetBankAccountsResponseDto>>> GetBankAccountsByCustomerId(int customerId)
        {
            var response = new ServiceResponse<List<GetBankAccountsResponseDto>>();
            var dbBankAccounts = await _db.BankAccounts
                .Include(ac => ac.Customer)
                .Where(ac => ac.CustomerId == customerId).ToListAsync(); //only receive the accounts that belong to the logged in Customer

            response.Data = dbBankAccounts.Select(acc => _mapper.Map<GetBankAccountsResponseDto>(acc)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetBankAccountsResponseDto>> TransferLoan(LoanDto loanDto)
        {
            var response = new ServiceResponse<GetBankAccountsResponseDto>();

            try
            {
                var accountTo = await _db.BankAccounts
                    .FirstOrDefaultAsync(ac => ac.CustomerId == loanDto.CustomerId);

                if (accountTo == null)
                {
                    throw new Exception($"Bank Account for the Customer with Id {loanDto.CustomerId} not found.");
                }

                accountTo.Balance = accountTo.Balance + loanDto.Amount;

                var newTransaction = new Transactions()
                {
                    BankAccountId = accountTo.Id,
                    Amount = loanDto.Amount,
                    Type = "loan",
                    TransactionDate = DateTime.UtcNow
                };

                _db.Transactions.Add(newTransaction);

                await _db.SaveChangesAsync();
                response.Data = _mapper.Map<GetBankAccountsResponseDto>(accountTo);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetBankAccountsResponseDto>> UpdateBankAccountBalance(TransferMoneyDto transferMoneyDto, int customerId)
        {
            var response = new ServiceResponse<GetBankAccountsResponseDto>();

            try
            {
                var accountFrom = await _db.BankAccounts
                    .FirstOrDefaultAsync(ac => ac.Id == transferMoneyDto.BankAccountFromId && ac.CustomerId == customerId);

                var accountTo = await _db.BankAccounts
                    .FirstOrDefaultAsync(ac => ac.Id == transferMoneyDto.BankAccountToId);

                if (accountFrom is null || accountTo == null || accountFrom.CustomerId != customerId)
                {
                    throw new Exception("Bank account number not found.");
                }

                if(accountFrom.Balance - transferMoneyDto.Amount < 0)
                {
                    throw new Exception("Not enough funds in account.");
                }

                accountFrom.Balance = accountFrom.Balance - transferMoneyDto.Amount;
                accountTo.Balance = accountTo.Balance + transferMoneyDto.Amount;

                var newTransactionFrom = new Transactions()
                {
                    BankAccountId = accountFrom.Id,
                    Amount = transferMoneyDto.Amount,
                    Type = "outflow",
                    TransactionDate = DateTime.UtcNow
                };

                _db.Transactions.Add(newTransactionFrom);

                var newTransactionTo = new Transactions()
                {
                    BankAccountId = accountTo.Id,
                    Amount = transferMoneyDto.Amount,
                    Type = "inflow",
                    TransactionDate = DateTime.UtcNow
                };

                _db.Transactions.Add(newTransactionTo);

                await _db.SaveChangesAsync();
                response.Data = _mapper.Map<GetBankAccountsResponseDto>(accountFrom);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
