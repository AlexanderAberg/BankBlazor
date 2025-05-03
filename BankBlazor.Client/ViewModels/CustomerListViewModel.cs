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

        public List<CustomerDTO> Customers { get; private set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; } = 12;



        public CustomerListViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoadCustomersAsync()
        {
            try
            {
                Console.WriteLine($"Fetching customers for page {CurrentPage} with page size {PageSize}...");
                var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<CustomerDTO>>($"api/customers/paginated?page={CurrentPage}&pageSize={PageSize}");
                if (response != null)
                {
                    Customers = response.Items;
                    TotalPages = (int)Math.Ceiling((double)response.TotalCount / PageSize);
                    Console.WriteLine($"Loaded {Customers.Count} customers. Total pages: {TotalPages}");
                }
                else
                {
                    Console.WriteLine("No response from API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading customers: {ex.Message}");
            }
        }



        public async Task<CustomerDTO?> GetCustomerByIdAsync(int customerId)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDTO>($"api/customers/{customerId}");
        }
    }

    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}

