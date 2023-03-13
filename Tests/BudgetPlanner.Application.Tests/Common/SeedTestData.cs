using Domain;
using Persistence;

namespace Application.Tests.Common
{
    public class SeedTestData
    {
        public static void Seed(DataContext context)
        {
            var budget = new Budget
            {
                Name = "test",
                User = UserCreator.CreateTestUser().Result
            };

            var accounts = new List<Domain.Account>
            {
                new Domain.Account
                {
                    Name = "CheckingTest",
                    AccountType = "Checking",
                    Balance = 1000,
                    Budget= budget
                },
                new Domain.Account
                {
                    Name = "SavingTest",
                    AccountType = "Saving",
                    Balance = 500,
                    Budget= budget
                }
            };

            var goal = new Goal
            {
                Name = "testGoal",
                CurrentAmount = 1000,
                Description = "Lorem ipsum",
                EndDate = DateTime.Now.AddDays(300),
                RequiredAmount = 2000,
                Budget = budget
            };

            context.Budgets.Add(budget);
            context.Accounts.AddRange(accounts);
            context.Goals.AddRange(goal);
            context.SaveChanges();
        }
    }
}