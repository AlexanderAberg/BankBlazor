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

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDTO request)
        {
            var account = await _context.Accounts.FindAsync(request.AccountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            account.Balance += request.Amount;

            await _context.SaveChangesAsync();

            return Ok("Deposit successful.");
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionDTO request)
        {
            var account = await _context.Accounts.FindAsync(request.AccountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            if (account.Balance < request.Amount)
            {
                return BadRequest("Insufficient funds.");
            }
            account.Balance -= request.Amount;
            await _context.SaveChangesAsync();
            return Ok("Withdrawal successful.");
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO transferRequest)
        {
            Console.WriteLine($"Transfer request received: SourceAccountId={transferRequest.AccountId}, Amount={transferRequest.Amount}, RecipientAccountNumber={transferRequest.RecipientAccountNumber}");

            var sourceAccount = await _context.Accounts.FindAsync(transferRequest.AccountId);
            if (sourceAccount == null)
            {
                return NotFound("Source account not found.");
            }

            var recipientAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountId.ToString() == transferRequest.RecipientAccountNumber);
            if (recipientAccount == null)
            {
                return NotFound("Recipient account not found.");
            }

            if (sourceAccount.Balance < transferRequest.Amount)
            {
                return BadRequest("Insufficient balance in the source account.");
            }

            sourceAccount.Balance -= transferRequest.Amount;
            recipientAccount.Balance += transferRequest.Amount;

            await _context.SaveChangesAsync();

            return Ok("Transfer successful.");
        }
    }
}
   

