namespace Application.DTO
{
    public class FutureExpenditureDto
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public double CompletedAmount { get; set; } = 0;
        public DateTime Date { get; set; }

        public string AccountName { get; set; }
        public IEnumerable<ExpenditureDto> CompletedExpenditures { get; set; } = new List<ExpenditureDto>();
    }
}