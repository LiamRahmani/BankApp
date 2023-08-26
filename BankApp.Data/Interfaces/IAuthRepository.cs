using BankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}
