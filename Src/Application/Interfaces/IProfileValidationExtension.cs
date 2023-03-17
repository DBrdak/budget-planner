namespace Application.Interfaces
{
    public interface IProfileValidationExtension
    {
        public Task<bool> UniqueUsername(string newUsername);

        public Task<bool> UniqueEmail(string newEmail);

        public Task<bool> UniqueBudgetName(string newBudgetName);
    }
}