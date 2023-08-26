using BankApp.Data.DataModels;
using BankApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using BankApp.Data.Interfaces;

namespace BankApp.Data.Repos
{
    public class AuthRepo : IAuthRepository
    {
        private readonly BankAppDBContext _context;

        private readonly IConfiguration _configuration;

        public AuthRepo(BankAppDBContext context, IConfiguration configuration)
        {
            _context = context;

            //to access apsettings.jsom file :
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();

            if (username == "admin" && password == "admin")
            {
                response.Data = CreateToken(0, username, UserRole.Admin);
                response.Success = true;
                return response;
            }
            var userToLogin = await _context.Customers.Where(user => user.Username == username).FirstOrDefaultAsync();

            if (userToLogin != null)
            {
                if(userToLogin.Password == password) {
                    response.Data = CreateToken(userToLogin.CustomerId, username, UserRole.Customer);
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "User not found. Wrong username or password.";
                }
            } else
            {
                response.Success = false;
                response.Message = "User not found, please register this user using a Admin account";
            }
            return response;
        }

        private string CreateToken(int id, string username, UserRole role)
        {
            //declare a list of claims 
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            //secret key from appsettings.json file
            var appsettingsToken = _configuration.GetSection("AppSettings:Token").Value;

            if (appsettingsToken is null)
            {
                throw new Exception("AppSettings Token is null!");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appsettingsToken));

            // create signing credentials where we  use sha512 signature algorithm
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // security token descripter - gets the information that is used to create the final token
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler(); // Jwt package provides support for creating, serializing and validationg JSON Web tokens.
            SecurityToken token = tokenHandler.CreateToken(tokenDescripter);

            // serialize the security token into a JSON web token and in the end we return exactly that
            return tokenHandler.WriteToken(token);
        }
    }
}
