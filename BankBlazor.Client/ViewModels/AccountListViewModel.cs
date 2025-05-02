using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BankBlazor.Client.DTOs;

namespace BankBlazor.Client.ViewModels
{
    public class AccountListViewModel
    {
        private readonly HttpClient _httpClient;

        public AccountListViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<AccountDTO> Accounts { get; set; } = new();
        public AccountDTO? CurrentAccount { get; set; }
        public int SelectedAccountId { get; set; }

        public async Task LoadAccountsAsync()
        {
            try
            {
                Console.WriteLine("Fetching accounts from API...");
                Accounts = await _httpClient.GetFromJsonAsync<List<AccountDTO>>("api/accounts") ?? new List<AccountDTO>();
                foreach (var account in Accounts)
                {
                    Console.WriteLine($"AccountId: {account.AccountId}, Frequency: {account.Frequency}, Balance: {account.Balance}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching accounts: {ex.Message}");
            }
        }


        public async Task LoadAccountDetailsAsync()
        {
            CurrentAccount = await _httpClient.GetFromJsonAsync<AccountDTO>($"api/accounts/{SelectedAccountId}");
        }

        public async Task DepositAsync(decimal amount)
        {
        }

        public async Task WithdrawAsync(decimal amount)
        {
        }

        public async Task TransferAsync(decimal amount, string recipientAccountNumber)
        {
        }
    }
}



