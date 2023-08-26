using BankApp.Domain.Models.Dtos;
using AutoMapper;
using BankApp.Domain.Models;

namespace BankApp.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<BankAccount, GetBankAccountsResponseDto>();
            CreateMap<BankAccountCreateDto, BankAccount>();
            CreateMap<Transactions, TransactionDto>();
        }
    }
}
