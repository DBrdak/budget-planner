using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// Past incomes and expenditures
/// </summary>

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    [Required]
    public string Title { get; set; }
    public DateTime Date { get; set; }
    [Required]
    public string Category { get; set; }

    public Guid AccountId { get; set; }
    public Account Account { get; set; }

    public Guid? FutureTransactionId { get; set; }
    public FutureTransaction FutureTransaction { get; set; }

    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; }
}