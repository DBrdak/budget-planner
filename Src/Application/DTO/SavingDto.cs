namespace Application.DTO;

public class SavingDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    public string FromAccountName { get; set; }

    public string ToAccountName { get; set; }

    public string GoalName { get; set; }
}