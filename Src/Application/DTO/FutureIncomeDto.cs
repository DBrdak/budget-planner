namespace Application.DTO
{
    public class FutureIncomeDto
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public double CompletedAmount { get; set; } = 0;
        public DateTime Date { get; set; }

        public string AccountName { get; set; }
        public IEnumerable<IncomeDto> CompletedIncomes { get; set; } = new List<IncomeDto>();
    }
}