
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;

namespace BankApp.Data.Interfaces
{
    public interface ITransactionRepo
    {
        Task<ServiceResponse<List<TransactionDto>>> GetTransactionsByCustomerId(int customerId);
    }
}
