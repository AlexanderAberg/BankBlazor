using Microsoft.AspNetCore.Mvc;
using BankBlazorAPI.Data.Entitites;
using BankBlazor.Api.Data.Contexts;
using BankBlazorAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankBlazorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly BankAppDataContext _context;

        public AccountsController(BankAppDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccounts()
        {
            var accounts = await _context.Accounts
                .Select(a => new AccountDTO
                {
                    AccountId = a.AccountId,
                    Frequency = a.Frequency,
                    Balance = a.Balance
                })
                .ToListAsync();

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetAccount(int id)
        {
            var account = await _context.Accounts
                .Where(a => a.AccountId == id)
                .Select(a => new AccountDTO
                {
                    AccountId = a.AccountId,
                    Frequency = a.Frequency,
                    Balance = a.Balance
                })
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }
    }
}
   

