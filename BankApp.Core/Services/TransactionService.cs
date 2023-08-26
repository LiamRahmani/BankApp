using BankApp.Domain.Models.Dtos;
using BankApp.Domain.Models;
using BankApp.Data.Interfaces;
using BankApp.Core.Interfaces;

namespace BankApp.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _repo;

        public TransactionService(ITransactionRepo repo)
        {
            _repo = repo;
        }
        public async Task<ServiceResponse<List<TransactionDto>>> GetTransactionsByCustomerId(int customerId)
        {
            return await _repo.GetTransactionsByCustomerId(customerId);
        }
    }
}
