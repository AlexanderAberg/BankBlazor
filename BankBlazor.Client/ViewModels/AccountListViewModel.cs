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
                Accounts = await _httpClient.GetFromJsonAsync<List<AccountDTO>>("api/accounts") ?? [];
                foreach (var account in Accounts)
                {
                }
            }
            catch (Exception)
            {
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
                    await LoadAccountDetailsAsync(); 
                }
            }
            catch (Exception)
            {
            }
        }


        public async Task WithdrawAsync(decimal amount)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/accounts/withdraw", new { AccountId = SelectedAccountId, Amount = amount });
                if (response.IsSuccessStatusCode)
                {
                    await LoadAccountDetailsAsync();
                }
            }
            catch (Exception)
            {
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
                    await LoadAccountDetailsAsync(); 
                }
            }
            catch (Exception)
            {
            }
        }
    }
}



