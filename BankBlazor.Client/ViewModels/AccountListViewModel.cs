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
            Accounts = await _httpClient.GetFromJsonAsync<List<AccountDTO>>("api/accounts") ?? new List<AccountDTO>();
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



