namespace Domain
{
    public class Saving
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
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
}