using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace BankBlazor.Client.ViewModels
{
    public class AccountListViewModel
    {
        private readonly HttpClient _httpClient;

        public AccountListViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<AccountListViewModel> Accounts { get; set; } = new();
        public AccountListViewModel? CurrentAccount { get; set; }
        public int SelectedAccountId { get; set; }

        public async Task LoadAccountsAsync()
        {
            Accounts = await _httpClient.GetFromJsonAsync<List<AccountListViewModel>>("api/accounts") ?? new List<AccountListViewModel>();
        }

        public async Task LoadAccountDetailsAsync()
        {
            CurrentAccount = await _httpClient.GetFromJsonAsync<AccountListViewModel>($"api/accounts/{SelectedAccountId}");
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

        public int AccountId { get; set; }
        public string Frequency { get; set; } = string.Empty;
        public DateOnly Created { get; set; }
        public decimal Balance { get; set; }
    }
}

