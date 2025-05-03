using Microsoft.AspNetCore.Mvc;
using BankBlazorAPI.Data.Entitites;
using BankBlazor.Api.Data.Contexts;
using BankBlazorAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using BankBlazor.Client.ViewModels;

namespace BankBlazorAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(BankAppDataContext context) : ControllerBase
    {
        [HttpGet("paginated")]
        public async Task<ActionResult<PaginatedResponse<CustomerDTO>>> GetPaginatedCustomers(int page = 1, int pageSize = 12)
        {
            var totalCount = await context.Customers.CountAsync();
            var customers = await context.Customers
                .OrderBy(c => c.CustomerId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Gender = c.Gender,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    Zipcode = c.Zipcode,
                    Country = c.Country,
                    CountryCode = c.CountryCode,
                    Birthday = c.Birthday,
                    NationalId = c.NationalId,
                    Telephonecountrycode = c.Telephonecountrycode,
                    Telephonenumber = c.Telephonenumber,
                    Emailaddress = c.Emailaddress

                })
                .ToListAsync();

            return Ok(new PaginatedResponse<CustomerDTO>
            {
                Items = customers,
                TotalCount = totalCount
            });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = await context.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Gender = c.Gender,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    Zipcode = c.Zipcode,
                    Country = c.Country,
                    CountryCode = c.CountryCode,
                    Birthday = c.Birthday,
                    NationalId = c.NationalId,
                    Telephonecountrycode = c.Telephonecountrycode,
                    Telephonenumber = c.Telephonenumber,
                    Emailaddress = c.Emailaddress
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
