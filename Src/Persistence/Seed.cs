using Bogus;
using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<User> userManager)
        {
            if (!userManager.Users.Any() && !context.Budgets.Any() && !context.Goals.Any() && !context.Accounts.Any() &&
                !context.FutureTransactions.Any() && !context.FutureSavings.Any() && !context.Transactions.Any() && !context.Savings.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        DisplayName="John",
                        UserName="john",
                        Email="john@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                var budgets = new List<Budget>
                {
                    new Budget
                    {
                        Name = "JohnnyBudget",
                        User = users[0]
                    }
                };

                var johnAccounts = new List<Account>
                {
                    new Account
                    {
                        Budget=budgets[0],
                        Name="Checking account 01",
                        AccountType="Checking",
                        Balance=2000
                    },
                    new Account
                    {
                        Budget=budgets[0],
                        Name="Checking account 02",
                        AccountType="Checking",
                        Balance=400
                    },
                    new Account
                    {
                        Budget=budgets[0],
                        Name="Saving account",
                        AccountType="Saving",
                        Balance=6500
                    },
                };

                var ffdate = DateTime.Today.AddDays(300);
                var goals = new List<Goal> {
                    new Goal
                    {
                        Name = "chuj",
                        EndDate = ffdate,
                        CurrentAmount = 234.21,
                        RequiredAmount = 222222.12,
                        Budget = budgets[0]
                    },
                    new Goal
                    {
                        Name = "chujec",
                        EndDate = ffdate.AddDays(325),
                        CurrentAmount = 1234.21,
                        RequiredAmount = 2112222.12,
                        Budget = budgets[0]
                    }
                };

                var fdate = DateTime.Today.AddDays(20);
                var date = new DateTime(2022, 12, 1);

                var fSav = new List<FutureSaving>
                {
                    new FutureSaving
                    {
                        FromAccount=johnAccounts[0],
                        ToAccount=johnAccounts[2],
                        Amount=1201.21,
                        Goal = goals[0],
                        Date = fdate,
                        Budget=budgets[0]
                    },
                    new FutureSaving
                    {
                        FromAccount=johnAccounts[1],
                        ToAccount=johnAccounts[2],
                        Amount=172.32,
                        Goal = goals[1],
                        Date = fdate.AddDays(5),
                        Budget=budgets[0],
                    },
                };

                var fTran = new List<FutureTransaction>
                {
                    new FutureTransaction
                    {
                        Category="Groceries",
                        Amount=-100,
                        Account=johnAccounts[0],
                        Budget=budgets[0],
                        Date=fdate
                    },
                    new FutureTransaction
                    {
                        Category="Drugs",
                        Amount=-8,
                        Account=johnAccounts[1],
                        Budget=budgets[0],
                        Date=fdate.AddDays(14)
                    },
                    new FutureTransaction
                    {
                        Category="Job",
                        Amount=1500,
                        Account=johnAccounts[0],
                        Budget=budgets[0],
                        Date=fdate.AddDays(20)
                    },
                    new FutureTransaction
                    {
                        Category="Blowjob",
                        Amount=200,
                        Account=johnAccounts[1],
                        Budget=budgets[0],
                        Date=fdate.AddDays(1)
                    }
                };

                var sav = new List<Saving>
                {
                    new Saving
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(1),
                        Amount=50,
                        FromAccount=johnAccounts[0],
                        ToAccount=johnAccounts[2],
                        Goal=goals[1],
                        FutureSaving=fSav[1]
                    },
                    new Saving
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(11),
                        Amount=200,
                        FromAccount=johnAccounts[1],
                        ToAccount=johnAccounts[2],
                        Goal=goals[0],
                        FutureSaving=fSav[0]
                    }
                };

                var categories = new List<TransactionCategory>
                {
                    new TransactionCategory
                    {
                        Value="Groceries",
                        Budget=budgets[0],
                        Type="expenditure"
                    },
                    new TransactionCategory
                    {
                        Value="Drugs",
                        Budget=budgets[0],
                        Type="expenditure"
                    },
                    new TransactionCategory
                    {
                        Value="Job",
                        Budget=budgets[0],
                        Type="income"
                    },
                    new TransactionCategory
                    {
                        Value="Blowjob",
                        Budget=budgets[0],
                        Type="income"
                    },
                };

                var tran = new List<Transaction>
                {
                    new Transaction
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(13),
                        Amount=-75,
                        Title="Biedra",
                        Category="Groceries",
                        Account=johnAccounts[0],
                        FutureTransaction = fTran[0]
                    },
                    new Transaction
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(28),
                        Amount=-50,
                        Title="Mefedron",
                        Category="Drugs",
                        Account=johnAccounts[1],
                        FutureTransaction = fTran[1]
                    },
                    new Transaction
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(13),
                        Amount=50,
                        Title="Prezes",
                        Category="Job",
                        Account=johnAccounts[0],
                        FutureTransaction = fTran[2]
                    },
                    new Transaction
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(28),
                        Amount=50,
                        Title="Prezes",
                        Category="Blowjob",
                        Account=johnAccounts[1],
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
//                .RuleFor(x => x.Balance, f => f.Random.Double(-150, 20000))
//                .RuleFor(x => x.Budget, f => budget[0]);

//            var accounts = accountGenerator.Generate(5);

//            // Goals

//            var goalGenerator = new Faker<Goal>()
//                .RuleFor(x => x.Budget, f => budget[0])
//                .RuleFor(x => x.EndDate, f => f.Date.Future(1, DateTime.Now))
//                .RuleFor(x => x.RequiredAmount, f => f.Random.Double(1000, 500000))
//                .RuleFor(x => x.CurrentAmount, f => f.Random.Double(0, 1000))
//                .RuleFor(x => x.Description, f => f.Lorem.Sentence(15))
//                .RuleFor(x => x.Name, f => f.Hacker.Noun());

//            var goals = goalGenerator.Generate(6);

//            // Future Transactions

//            var futureTransactionGenerator = new Faker<FutureTransaction>()
//                .RuleFor(x => x.Budget, f => budget[0])
//                .RuleFor(x => x.Account, f => f.PickRandom(accounts.Where(a => a.AccountType == "Checking")))
//                .RuleFor(x => x.Date, f => f.Date.Future(0, DateTime.UtcNow.AddDays(-30)))
//                .RuleFor(x => x.Category, f => f.Hacker.Noun())
//                .RuleFor(x => x.Amount, f => f.Random.Double(-2000, 5000));

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
//                .RuleFor(x => x.Amount, f => f.Random.Double(-200, 5000));
//        }