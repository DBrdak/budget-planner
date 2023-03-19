using System.ComponentModel.DataAnnotations;

namespace Domain;

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