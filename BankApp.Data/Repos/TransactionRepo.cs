using AutoMapper;
using BankApp.Data.DataModels;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models.Dtos;
using BankApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly BankAppDBContext _db;
        private readonly IMapper _mapper;

        public TransactionRepo(BankAppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<TransactionDto>>> GetTransactionsByCustomerId(int customerId)
        {
            var response = new ServiceResponse<List<TransactionDto>>();

            var dbTransactions = await _db.Transactions
                .Include(t => t.BankAccount)
                .Where(t => t.BankAccount!.CustomerId == customerId).ToListAsync(); //only receive the Transactions that belong to the logged in Customer´s Bank Accounts

            response.Data = dbTransactions.Select(acc => _mapper.Map<TransactionDto>(acc)).ToList();
            return response;
        }
    }
}
