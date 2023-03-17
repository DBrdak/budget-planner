namespace Domain
{
    public class TransactionCategory
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}