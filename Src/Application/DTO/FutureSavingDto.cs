namespace Application.DTO;

public class FutureSavingDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public decimal CompletedAmount { get; set; } = 0;
    public DateTime Date { get; set; }
    public string FromAccountName { get; set; }
    public string ToAccountName { get; set; }

    public string GoalName { get; set; }
    public IEnumerable<SavingDto> CompletedSavings { get; set; } = new List<SavingDto>();
}