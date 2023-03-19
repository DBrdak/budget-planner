namespace Application.DTO;

public class FutureExpenditureDto
{
    public Guid Id { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public decimal CompletedAmount { get; set; } = 0;
    public DateTime Date { get; set; }

    public string AccountName { get; set; }
    public IEnumerable<ExpenditureDto> CompletedExpenditures { get; set; } = new List<ExpenditureDto>();
}