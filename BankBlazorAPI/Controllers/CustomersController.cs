using Microsoft.AspNetCore.Mvc;
using BankBlazorAPI.Data.Entitites;
using BankBlazor.Api.Data.Contexts;
using BankBlazorAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankBlazorAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly BankAppDataContext _context;

        public CustomersController(BankAppDataContext context)
        {
            _context = context;
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetPaginatedCustomers(int page = 1, int pageSize = 10)
        {
            var customers = await _context.Customers
                .OrderBy(c => c.CustomerId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = await _context.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Emailaddress = c.Emailaddress,
                    Telephonenumber = c.Telephonenumber
                })
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
