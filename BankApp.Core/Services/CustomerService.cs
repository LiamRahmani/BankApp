using BankApp.Core.Interfaces;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;

namespace BankApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _repo;
        private readonly IBankAccountRepo _bankAccRepo;

        public CustomerService(ICustomerRepo repo, IBankAccountRepo bankAccRepo)
        {
            _repo = repo;
            _bankAccRepo = bankAccRepo; 
        }

        public async Task<ServiceResponse<List<Customer>>> AddNewCustomer(CustomerCreateDto customer)
        {
            var response = await _repo.AddNewCustomer(customer);

            var bankAccount = new BankAccountCreateDto()
            {
                Type = "personal",
                Balance = 0
            };

            await _bankAccRepo.AddNewBankAccount(bankAccount, await _repo.LastAddedCustomerId());

            return response;
        }

        public async Task<ServiceResponse<List<Customer>>> GetCustomerList()
        {
            return await _repo.GetCustomerList();
        }
    }
}
