using AutoMapper;
using BankApp.Data.DataModels;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BankApp.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankAppDBContext _db;
        private readonly IMapper _mapper;

        public CustomerRepo(BankAppDBContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> LastAddedCustomerId()
        {
            return await _db.Customers.MaxAsync(c => c.CustomerId);
        }

        public async Task<ServiceResponse<List<Customer>>> AddNewCustomer(CustomerCreateDto newCustomer)
        {
            var response = new ServiceResponse<List<Customer>>();
            var character = _mapper.Map<Customer>(newCustomer);

            _db.Customers.Add(character);
            await _db.SaveChangesAsync();

            response.Data = await _db.Customers.ToListAsync();
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<List<Customer>>> GetCustomerList()
        {
            var response = new ServiceResponse<List<Customer>>();
            response.Data = await _db.Customers.Include(c => c.BankAccounts).ToListAsync();
            response.Success = true;
            return response;
        }
    }
}
