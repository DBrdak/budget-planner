using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Goal
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }

    public decimal CurrentAmount { get; set; }

    public decimal RequiredAmount { get; set; }

    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; }

    public IEnumerable<Saving> Savings { get; set; } = new List<Saving>();
}