namespace BankApp.Domain.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public BankAccount? BankAccount { get; set;}
    }
}
