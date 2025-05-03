using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BankBlazor.Client.DTOs;

namespace BankBlazor.Client.ViewModels
{
    public class CustomerListViewModel
    {
        private readonly HttpClient _httpClient;

        public CustomerListViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<CustomerDTO> Customers { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public async Task LoadCustomersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CustomerDTO>>($"api/customers/paginated?page={CurrentPage}&pageSize={PageSize}");
            if (response != null)
            {
                Customers = response;
            }
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _httpClient.GetFromJsonAsync<CustomerDTO>($"api/customers/{id}");
                Console.WriteLine($"Fetched customer: {customer?.Givenname} {customer?.Surname}");
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching customer: {ex.Message}");
                return null;
            }
        }
    }
}
