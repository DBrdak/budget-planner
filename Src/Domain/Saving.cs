namespace Domain;

/// <summary>
/// Past savings that user has done
/// </summary>

public class Saving
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public Guid FromAccountId { get; set; }
    public Account FromAccount { get; set; }

    public Guid ToAccountId { get; set; }
    public Account ToAccount { get; set; }

    public Guid? GoalId { get; set; }
    public Goal Goal { get; set; }

    public Guid? FutureSavingId { get; set; }

    public FutureSaving FutureSaving { get; set; }

    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; }
}