namespace Application.DTO;

public class AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }

    public IEnumerable<SavingDto> SavingsOut { get; set; }
    public IEnumerable<SavingDto> SavingsIn { get; set; }
    public IEnumerable<IncomeDto> Incomes { get; set; }
    public IEnumerable<ExpenditureDto> Expenditures { get; set; }
}