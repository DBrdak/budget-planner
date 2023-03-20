using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// Expenditure and income categories that user specifies when adding new FutureTransaction
/// </summary>

public class TransactionCategory
{
    public Guid Id { get; set; }
    [Required]
    public string Value { get; set; }
    [Required]
    public string Type { get; set; }

    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; }
}