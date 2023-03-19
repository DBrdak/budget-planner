namespace Domain;

public class Account
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }

    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; }

    public IEnumerable<FutureSaving> FutureSavingsOut { get; set; } = new List<FutureSaving>();
    public IEnumerable<FutureSaving> FutureSavingsIn { get; set; } = new List<FutureSaving>();
    public IEnumerable<FutureTransaction> FutureTransactions { get; set; } = new List<FutureTransaction>();
    public IEnumerable<Saving> SavingsOut { get; set; } = new List<Saving>();
    public IEnumerable<Saving> SavingsIn { get; set; } = new List<Saving>();
    public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
}