using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;

namespace BankApp.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResponse<List<Customer>>> AddNewCustomer(CustomerCreateDto customer);
        Task<ServiceResponse<List<Customer>>> GetCustomerList();
    }
}
