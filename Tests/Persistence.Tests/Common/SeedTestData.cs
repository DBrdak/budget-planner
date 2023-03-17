using Domain;
using Persistence;
using Persistence.Tests.Common;

namespace Persistence.Tests.Common
{
    public static class SeedTestData
    {
        public static void Seed(this DataContext context)
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
                    Budget = budget
                },
                new Domain.Account
                {
                    Name = "SavingTest",
                    AccountType = "Saving",
                    Balance = 500,
                    Budget = budget
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

            var futureTransactions = new List<FutureTransaction>
            {
                new FutureTransaction
                {
                    Budget = budget,
                    Account = accounts[0],
                    Amount = -500,
                    Category = "Groceries",
                    Date = DateTime.UtcNow.AddDays(-30)
                },
                new FutureTransaction
                {
                    Budget = budget,
                    Account = accounts[0],
                    Amount = 200,
                    Category = "Job",
                    Date = DateTime.UtcNow.AddDays(-20)
                },
                new FutureTransaction
                {
                    Budget = budget,
                    Account = accounts[0],
                    Amount = -100,
                    Category = "Transport",
                    Date = DateTime.UtcNow.AddDays(20)
                },
                new FutureTransaction
                {
                    Budget = budget,
                    Account = accounts[0],
                    Amount = 200,
                    Category = "Freelance",
                    Date = DateTime.UtcNow.AddDays(15)
                },
            };

            var futureSavings = new List<FutureSaving>
            {
                new FutureSaving
                {
                    Budget = budget,
                    FromAccount = accounts[0],
                    ToAccount = accounts[1],
                    Amount = 200,
                    Goal = goal,
                    Date = DateTime.UtcNow.AddDays(10),
                },
                new FutureSaving
                {
                    Budget = budget,
                    FromAccount = accounts[0],
                    ToAccount = accounts[1],
                    Amount = 200,
                    Goal = goal,
                    Date = DateTime.UtcNow.AddDays(-5),
                }
            };

            var transaction = new List<Transaction>
            {
                new Transaction
                {
                    Budget = budget,
                    Account = accounts[0],
                    Amount = 190,
                    Category = "Job",
                    FutureTransaction = futureTransactions[1],
                    Date = DateTime.UtcNow.AddDays(-2),
                    Title = "Boss"
                },
                new Transaction
                {
                    Budget = budget,
                    Account = accounts[0],
                    Amount = -220,
                    Category = "Groceries",
                    FutureTransaction = futureTransactions[0],
                    Date = DateTime.UtcNow.AddDays(-25),
                    Title = "Lidl"
                },
                new Transaction
                {
                    Budget = budget,
                    Account = accounts[0],
                    Amount = -190,
                    Category = "Groceries",
                    FutureTransaction = futureTransactions[0],
                    Date = DateTime.UtcNow.AddDays(-14),
                    Title = "Auchan"
                },
            };

            var savings = new List<Saving>
            {
                new Saving
                {
                    Amount = 400,
                    Budget = budget,
                    Date = DateTime.UtcNow.AddDays(-4),
                    Goal = goal,
                    FutureSaving = futureSavings[0],
                    FromAccount = accounts[0],
                    ToAccount = accounts[1],
                },
                new Saving
                {
                    Amount = 130,
                    Budget = budget,
                    Date = DateTime.UtcNow.AddDays(-4),
                    Goal = goal,
                    FutureSaving = futureSavings[0],
                    FromAccount = accounts[0],
                    ToAccount = accounts[1],
                }
            };

            var categories = new List<TransactionCategory>
            {
                new TransactionCategory
                {
                    Budget = budget,
                    Value = futureTransactions[0].Category,
                    Type = "expenditure"
                },
                new TransactionCategory
                {
                    Budget = budget,
                    Value = futureTransactions[1].Category,
                    Type = "income"
                },
                new TransactionCategory
                {
                    Budget = budget,
                    Value = futureTransactions[2].Category,
                    Type = "expenditure"
                },
                new TransactionCategory
                {
                    Budget = budget,
                    Value = futureTransactions[3].Category,
                    Type = "income"
                },
            };

            context.Budgets.Add(budget);
            context.Accounts.AddRange(accounts);
            context.Goals.AddRange(goal);
            context.FutureSavings.AddRange(futureSavings);
            context.FutureTransactions.AddRange(futureTransactions);
            context.TransactionCategories.AddRange(categories);
            context.Transactions.AddRange(transaction);
            context.Savings.AddRange(savings);
            context.SaveChanges();
        }
    }
}