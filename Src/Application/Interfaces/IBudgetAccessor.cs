using Domain;

namespace Application.Interfaces;

public interface IBudgetAccessor
{
    Task<string> GetBudgetName();

    Task<Guid> GetBudgetId();

    Task<Budget> GetBudget();
}