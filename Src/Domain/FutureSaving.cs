﻿namespace Domain;

/// <summary>
/// Savings that user has planned for future
/// </summary>

public class FutureSaving
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }

    public decimal CompletedAmount { get; set; }

    public DateTime Date { get; set; }

    public Goal Goal { get; set; }

    public Guid FromAccountId { get; set; }
    public Account FromAccount { get; set; }
    public Guid ToAccountId { get; set; }
    public Account ToAccount { get; set; }

    public IEnumerable<Saving> CompletedSavings { get; set; } = new List<Saving>();

    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; }
}