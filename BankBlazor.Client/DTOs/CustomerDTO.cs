namespace BankBlazor.Client.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Emailaddress { get; set; } = string.Empty;
        public string Telephonenumber { get; set; } = string.Empty;
    }
}
