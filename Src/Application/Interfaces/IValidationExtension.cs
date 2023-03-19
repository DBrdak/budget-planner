namespace Application.Interfaces;

public interface IValidationExtension
{
    public Task<bool> UniqueCategory(string categoryName, string categoryType);

    public Task<bool> CategoryExists(string categoryName, string categoryType);

    public Task<bool> AccountExists(string accountName);

    public Task<bool> AccountTypeOf(string accountName, string accountType);

    public Task<bool> UniqueAccountName(string accountName);

    public Task<bool> UniqueGoalName(string goalName);

    public Task<bool> GoalExists(string goalName);
}