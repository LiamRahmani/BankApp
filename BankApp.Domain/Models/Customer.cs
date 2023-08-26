namespace BankApp.Domain.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string TelephoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public List<BankAccount>? BankAccounts { get; set; }
    }
}
