using BankApp.Core.Interfaces;
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BankAccountController(IBankAccountService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        // we use this method in get all Bank accounts for this user
        private int GetLoggedInCustomerId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        private string GetLoggedInUserRole() => _httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.Role)!;

        [HttpGet("GetBankAccountsByCustomerId")]
        public async Task<ActionResult<ServiceResponse<List<GetBankAccountsResponseDto>>>> GetBankAccountsByCustomerId()
        {
            string roleValue = GetLoggedInUserRole();
            if (roleValue == "Admin")
            {
                return StatusCode(401, "You are logged in as admin and you don´t have your bank accounts.");
            }
            if (roleValue == "Customer")
            {
                var customerId = GetLoggedInCustomerId();
                var response = await _service.GetBankAccountsByCustomerId(customerId);
                return Ok(response);
            }

            return Unauthorized();
        }

        [HttpPost("AddNewBankAccount")]
        public async Task<ActionResult<ServiceResponse<List<GetBankAccountsResponseDto>>>> AddNewBankAccount(BankAccountCreateDto bankAccount)
        {
            try
            {
                string roleValue = GetLoggedInUserRole();
                if (roleValue == "Customer")
                {
                    var customerId = GetLoggedInCustomerId();
                    var response = await _service.AddNewBankAccount(bankAccount, customerId);
                    return Ok(response);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateBankAccountBalance")]
        public async Task<ActionResult<ServiceResponse<GetBankAccountsResponseDto>>> UpdateBankAccountBalance(TransferMoneyDto transferMoneyDto)
        {
            var customerId = GetLoggedInCustomerId();
            var response = await _service.UpdateBankAccountBalance(transferMoneyDto, customerId);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPut("TransferLoan")]
        public async Task<ActionResult<ServiceResponse<GetBankAccountsResponseDto>>> TransferLoan(LoanDto loanDto)
        {
            try
            {
                string roleValue = GetLoggedInUserRole();
                if (roleValue == "Admin")
                {
                    var customerId = GetLoggedInCustomerId();
                    var response = await _service.TransferLoan(loanDto);

                    if (response.Data is null)
                    {
                        return NotFound(response);
                    }
                    else
                    {
                        return Ok(response);
                    }
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
