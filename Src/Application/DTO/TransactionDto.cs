namespace Application.DTO;

public class TransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }

    public string AccountName { get; set; }
}