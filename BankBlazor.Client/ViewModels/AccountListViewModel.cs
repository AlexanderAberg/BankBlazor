using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BankBlazor.Client.DTOs;

namespace BankBlazor.Client.ViewModels
{
    public class AccountListViewModel(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public List<AccountDTO> Accounts { get; set; } = [];
        public AccountDTO? CurrentAccount { get; set; }
        public int SelectedAccountId { get; set; }

        public async Task LoadAccountsAsync()
        {
            try
            {
                Console.WriteLine("Fetching accounts from API...");
                Accounts = await _httpClient.GetFromJsonAsync<List<AccountDTO>>("api/accounts") ?? [];
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
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/accounts/deposit", new { AccountId = SelectedAccountId, Amount = amount });
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Deposit successful.");
                    await LoadAccountDetailsAsync(); 
                }
                else
                {
                    Console.WriteLine($"Deposit failed: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deposit: {ex.Message}");
            }
        }


        public async Task WithdrawAsync(decimal amount)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/accounts/withdraw", new { AccountId = SelectedAccountId, Amount = amount });
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Withdrawal successful.");
                    await LoadAccountDetailsAsync();
                }
                else
                {
                    Console.WriteLine($"Withdrawal failed: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during withdrawal: {ex.Message}");
            }
        }

        public async Task TransferAsync(decimal amount, string recipientAccountNumber)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/accounts/transfer", new TransferDTO
                {
                    AccountId = SelectedAccountId,
                    Amount = amount,
                    RecipientAccountNumber = recipientAccountNumber
                });

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Transfer successful.");
                    await LoadAccountDetailsAsync(); 
                }
                else
                {
                    Console.WriteLine($"Transfer failed: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during transfer: {ex.Message}");
            }
        }
    }
}



