namespace Application.DTO;

public class GoalDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
    public decimal CurrentAmount { get; set; }
    public decimal RequiredAmount { get; set; }
}