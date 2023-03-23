using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// Incomes and expenditures that user has planned for future
/// </summary>

public class FutureTransaction
{
    public Guid Id { get; set; }
    [Required]
    public string Category { get; set; }

    public decimal Amount { get; set; }

    public decimal CompletedAmount { get; set; }

    public DateTime Date { get; set; }

    public Guid AccountId { get; set; }
    public Account Account { get; set; }

    public IEnumerable<Transaction> CompletedTransactions { get; set; } = new List<Transaction>();

    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; }
}