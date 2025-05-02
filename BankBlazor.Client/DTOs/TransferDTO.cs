namespace BankBlazor.Client.DTOs
{
    public class TransferDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string RecipientAccountNumber { get; set; } = string.Empty;
    }
}