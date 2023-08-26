using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;

namespace BankApp.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<ServiceResponse<List<Customer>>> AddNewCustomer(CustomerCreateDto customer);
        Task<ServiceResponse<List<Customer>>> GetCustomerList();
        Task<int> LastAddedCustomerId();
    }
}
