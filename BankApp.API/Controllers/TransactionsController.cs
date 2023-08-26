using BankApp.Core.Interfaces;
using BankApp.Domain.Models.Dtos;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionsController(ITransactionService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetLoggedInCustomerId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
        private string GetLoggedInUserRole() => _httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.Role)!;

        [HttpGet("GetTransactionsForLoggedInCustomer")]
        public async Task<ActionResult<ServiceResponse<List<TransactionDto>>>> GetTransactionsByCustomerId()
        {
            string roleValue = GetLoggedInUserRole();
            if (roleValue == "Admin")
            {
                return StatusCode(401, "You are logged in as admin and you don´t have your bank accounts and transactions.");
            }
            if (roleValue == "Customer")
            {
                var customerId = GetLoggedInCustomerId();
                var response = await _service.GetTransactionsByCustomerId(customerId);
                return Ok(response);
            }

            return Unauthorized();
        }
    }
}
