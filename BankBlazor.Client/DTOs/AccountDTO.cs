namespace BankBlazor.Client.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; } = string.Empty;
        public decimal Balance { get; set; }

    }
}
