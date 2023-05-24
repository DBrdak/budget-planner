using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context,
        UserManager<User> userManager)
    {
        if (!userManager.Users.Any() && !context.Budgets.Any() && !context.Goals.Any() && !context.Accounts.Any() &&
            !context.FutureTransactions.Any() && !context.FutureSavings.Any() && !context.Transactions.Any() &&
            !context.Savings.Any())
        {
            var users = new List<User>
            {
                new()
                {
                    DisplayName = "John",
                    UserName = "john",
                    Email = "john@test.com"
                }
            };

            foreach (var user in users)
                await userManager.CreateAsync(user, "Pa$$w0rd");

            var budgets = new List<Budget>
            {
                new()
                {
                    Name = "JohnnyBudget",
                    User = users[0]
                }
            };

            var johnAccounts = new List<Account>
            {
                new()
                {
                    Budget = budgets[0],
                    Name = "Checking account 01",
                    AccountType = "Checking",
                    Balance = 2000
                },
                new()
                {
                    Budget = budgets[0],
                    Name = "Checking account 02",
                    AccountType = "Checking",
                    Balance = 400
                },
                new()
                {
                    Budget = budgets[0],
                    Name = "Saving account",
                    AccountType = "Saving",
                    Balance = 6500
                }
            };

            var ffdate = DateTime.Today.AddDays(300);
            var goals = new List<Goal>
            {
                new()
                {
                    Name = "Vacation",
                    EndDate = ffdate,
                    RequiredAmount = 222222.12m,
                    Budget = budgets[0]
                },
                new()
                {
                    Name = "New Car",
                    EndDate = ffdate.AddDays(325),
                    RequiredAmount = 2112222.12m,
                    Budget = budgets[0]
                }
            };

            var fdate = DateTime.Today.AddDays(20);
            var date = new DateTime(2022, 12, 1);

            var sav = new List<Saving>
            {
                new()
                {
                    Budget = budgets[0],
                    Date = date.AddDays(1),
                    Amount = 50,
                    FromAccount = johnAccounts[0],
                    ToAccount = johnAccounts[2],
                    Goal = goals[1]
                },
                new()
                {
                    Budget = budgets[0],
                    Date = date.AddDays(11),
                    Amount = 200,
                    FromAccount = johnAccounts[1],
                    ToAccount = johnAccounts[2],
                    Goal = goals[0]
                }
            };

            var fSav = new List<FutureSaving>
            {
                new()
                {
                    FromAccount = johnAccounts[0],
                    ToAccount = johnAccounts[2],
                    Amount = 1201.21m,
                    Goal = goals[0],
                    Date = fdate,
                    Budget = budgets[0],
                    CompletedSavings = new List<Saving>
                    {
                        sav[1]
                    }
                },
                new()
                {
                    FromAccount = johnAccounts[1],
                    ToAccount = johnAccounts[2],
                    Amount = 172.32m,
                    Goal = goals[1],
                    Date = fdate.AddDays(5),
                    Budget = budgets[0],
                    CompletedSavings = new List<Saving>
                    {
                        sav[0]
                    }
                },
                new()
                {
                    FromAccount = johnAccounts[0],
                    ToAccount = johnAccounts[2],
                    Amount = 1201.21m,
                    Goal = goals[0],
                    Date = fdate.AddYears(1),
                    Budget = budgets[0],
                    CompletedSavings = new List<Saving>
                    {
                        sav[1]
                    }
                },
                new()
                {
                    FromAccount = johnAccounts[1],
                    ToAccount = johnAccounts[2],
                    Amount = 172.32m,
                    Goal = goals[1],
                    Date = fdate.AddYears(1).AddDays(2),
                    Budget = budgets[0],
                    CompletedSavings = new List<Saving>
                    {
                        sav[0]
                    }
                },
                new()
                {
                    FromAccount = johnAccounts[0],
                    ToAccount = johnAccounts[2],
                    Amount = 1201.21m,
                    Goal = goals[0],
                    Date = fdate.AddMonths(3).AddDays(-5),
                    Budget = budgets[0],
                    CompletedSavings = new List<Saving>
                    {
                        sav[1]
                    }
                },
                new()
                {
                    FromAccount = johnAccounts[1],
                    ToAccount = johnAccounts[2],
                    Amount = 172.32m,
                    Goal = goals[1],
                    Date = fdate.AddMonths(3),
                    Budget = budgets[0],
                    CompletedSavings = new List<Saving>
                    {
                        sav[0]
                    }
                }
            };

            var fTran = new List<FutureTransaction>
            {
                new()
                {
                    Category = "Groceries",
                    Amount = -100,
                    Account = johnAccounts[0],
                    Budget = budgets[0],
                    Date = fdate
                },
                new()
                {
                    Category = "Transport",
                    Amount = -8,
                    Account = johnAccounts[1],
                    Budget = budgets[0],
                    Date = fdate.AddDays(14)
                },
                new()
                {
                    Category = "Job",
                    Amount = 1500,
                    Account = johnAccounts[0],
                    Budget = budgets[0],
                    Date = fdate.AddDays(20)
                },
                new()
                {
                    Category = "Freelance",
                    Amount = 200,
                    Account = johnAccounts[1],
                    Budget = budgets[0],
                    Date = fdate.AddDays(1)
                },
                new()
                {
                    Category = "Groceries",
                    Amount = -100,
                    Account = johnAccounts[0],
                    Budget = budgets[0],
                    Date = fdate.AddYears(1).AddDays(-12)
                },
                new()
                {
                    Category = "Transport",
                    Amount = -8,
                    Account = johnAccounts[1],
                    Budget = budgets[0],
                    Date = fdate.AddYears(1).AddDays(-3)
                },
                new()
                {
                    Category = "Job",
                    Amount = 1500,
                    Account = johnAccounts[0],
                    Budget = budgets[0],
                    Date = fdate.AddYears(1).AddDays(-9)
                },
                new()
                {
                    Category = "Freelance",
                    Amount = 200,
                    Account = johnAccounts[1],
                    Budget = budgets[0],
                    Date = fdate.AddYears(1)
                },
                new()
                {
                    Category = "Groceries",
                    Amount = -100,
                    Account = johnAccounts[0],
                    Budget = budgets[0],
                    Date = fdate.AddMonths(3)
                },
                new()
                {
                    Category = "Transport",
                    Amount = -8,
                    Account = johnAccounts[1],
                    Budget = budgets[0],
                    Date = fdate.AddMonths(3).AddDays(2)
                },
                new()
                {
                    Category = "Job",
                    Amount = 1500,
                    Account = johnAccounts[0],
                    Budget = budgets[0],
                    Date = fdate.AddMonths(3).AddDays(-4)
                },
                new()
                {
                    Category = "Freelance",
                    Amount = 200,
                    Account = johnAccounts[1],
                    Budget = budgets[0],
                    Date = fdate.AddMonths(3).AddDays(2)
                },
            };

            var categories = new List<TransactionCategory>
            {
                new()
                {
                    Value = "Groceries",
                    Budget = budgets[0],
                    Type = "expenditure"
                },
                new()
                {
                    Value = "Transport",
                    Budget = budgets[0],
                    Type = "expenditure"
                },
                new()
                {
                    Value = "Job",
                    Budget = budgets[0],
                    Type = "income"
                },
                new()
                {
                    Value = "Freelance",
                    Budget = budgets[0],
                    Type = "income"
                }
            };

            var tran = new List<Transaction>
            {
                new()
                {
                    Budget = budgets[0],
                    Date = date.AddDays(13),
                    Amount = -75,
                    Title = "Biedra",
                    Category = "Groceries",
                    Account = johnAccounts[0],
                    FutureTransaction = fTran[0]
                },
                new()
                {
                    Budget = budgets[0],
                    Date = date.AddDays(28),
                    Amount = -50,
                    Title = "Fuel",
                    Category = "Transport",
                    Account = johnAccounts[1],
                    FutureTransaction = fTran[1]
                },
                new()
                {
                    Budget = budgets[0],
                    Date = date.AddDays(13),
                    Amount = 50,
                    Title = "Prezes",
                    Category = "Job",
                    Account = johnAccounts[0],
                    FutureTransaction = fTran[2]
                },
                new()
                {
                    Budget = budgets[0],
                    Date = date.AddDays(28),
                    Amount = 50,
                    Title = "Company",
                    Category = "Frelance",
                    Account = johnAccounts[1],
                    FutureTransaction = fTran[3]
                }
            };

            await context.TransactionCategories.AddRangeAsync(categories);
            await context.Budgets.AddRangeAsync(budgets);
            await context.Accounts.AddRangeAsync(johnAccounts);
            await context.Goals.AddRangeAsync(goals);
            await context.Transactions.AddRangeAsync(tran);
            await context.Savings.AddRangeAsync(sav);
            await context.FutureTransactions.AddRangeAsync(fTran);
            await context.FutureSavings.AddRangeAsync(fSav);
            await context.SaveChangesAsync();
        }
    }
}

//        if (!userManager.Users.Any() && !context.Budgets.Any() && !context.Goals.Any() && !context.Accounts.Any() &&
//!context.FutureTransactions.Any() && !context.FutureSavings.Any() && !context.Transactions.Any() && !context.Savings.Any())
//        {
//            var random = new Random(1);

//            // Users

//            var userGenerator = new Faker<User>()
//                .RuleFor(x => x.DisplayName, f => f.Person.FirstName)
//                .RuleFor(x => x.UserName, f => f.Person.FirstName.ToLower())
//                .RuleFor(x => x.Email, f => f.Person.Email);

//            var user = userGenerator.Generate(1);

//            await userManager.CreateAsync(user[0], "Pa$$w0rd");

//            // Budgets

//            var budgetGenerator = new Faker<Budget>()
//                .RuleFor(x => x.Name, f => f.Name.FirstName() + "budget");

//            var budget = budgetGenerator.Generate(1);

//            budget[0].User = user[0];

//            // Accounts

//            var accountGenerator = new Faker<Account>()
//                .RuleFor(x => x.AccountType, f => f.PickRandom(new List<string> { "Saving", "Checking" }))
//                .RuleFor(x => x.Name, f => f.Hacker.Noun())
//                .RuleFor(x => x.Balance, f => f.Random.decimal(-150, 20000))
//                .RuleFor(x => x.Budget, f => budget[0]);

//            var accounts = accountGenerator.Generate(5);

//            // Goals

//            var goalGenerator = new Faker<Goal>()
//                .RuleFor(x => x.Budget, f => budget[0])
//                .RuleFor(x => x.EndDate, f => f.Date.Future(1, DateTime.Now))
//                .RuleFor(x => x.RequiredAmount, f => f.Random.decimal(1000, 500000))
//                .RuleFor(x => x.CurrentAmount, f => f.Random.decimal(0, 1000))
//                .RuleFor(x => x.Description, f => f.Lorem.Sentence(15))
//                .RuleFor(x => x.Name, f => f.Hacker.Noun());

//            var goals = goalGenerator.Generate(6);

//            // Future Transactions

//            var futureTransactionGenerator = new Faker<FutureTransaction>()
//                .RuleFor(x => x.Budget, f => budget[0])
//                .RuleFor(x => x.Account, f => f.PickRandom(accounts.Where(a => a.AccountType == "Checking")))
//                .RuleFor(x => x.Date, f => f.Date.Future(0, DateTime.UtcNow.AddDays(-30)))
//                .RuleFor(x => x.Category, f => f.Hacker.Noun())
//                .RuleFor(x => x.Amount, f => f.Random.decimal(-2000, 5000));

//            var futureTransactions = futureTransactionGenerator.Generate(150);

//            // Transaction Categories

//            var categories = new List<TransactionCategory>();

//            foreach (var futureTransaction in futureTransactions)
//            {
//                var category = new TransactionCategory
//                {
//                    Value = futureTransaction.Category,
//                    Type = futureTransaction.Amount < 0 ? "expenditure" : "income"
//                };

//                categories.Add(category);
//            }

//            // Transactions

//            var transactionGenerator = new Faker<Transaction>()
//                .RuleFor(x => x.Budget, f => budget[0])
//                .RuleFor(x => x.Date, f => f.Date.Past(0, DateTime.Now))
//                .RuleFor(x => x.Category, f => f.PickRandom(categories.Select(c => c.Value)))
//                .RuleFor(x => x.Amount, f => f.Random.decimal(-200, 5000));
//        }