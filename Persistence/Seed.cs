using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var johnAccounts = new List<Account>
                {
                    new Account
                    {
                        Name="Checking account 01",
                        AccountType="Checking",
                        Balance=2000
                    },
                    new Account
                    {
                        Name="Checking account 02",
                        AccountType="Checking",
                        Balance=400
                    },
                    new Account
                    {
                        Name="Saving account",
                        AccountType="Saving",
                        Balance=6500
                    },
                };
                var johnGoals = new List<Goal>();
                var johnFutureSavings = new List<FutureSaving>
                {
                };
                var johnFutureTransactions = new List<FutureTransaction>
                {
                };
                var johnSavings = new List<Saving>
                {
                };
                var johnTransaction = new List<Transaction>
                {
                };

                var budgets = new List<Budget>
                {
                    new Budget
                    {
                        Name="JohnnyBudget",
                    },
                    new Budget
                    {
                        Name="BobBudget"
                    },
                    new Budget
                    {
                        Name="JaneBudget"
                    }
                };

                var users = new List<User>
                {
                    new User
                    {
                        DisplayName="John",
                        UserName="john",
                        Email="john@test.com",
                        Budget = budgets[0]
                    },
                    new User
                    {
                        DisplayName="Bob",
                        UserName="bob",
                        Email="bob@test.com",
                        Budget = budgets[1]
                    },
                    new User
                    {
                        DisplayName="Jane",
                        UserName="jane",
                        Email="jane@test.com",
                        Budget = budgets[2]
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                budgets[0].Accounts = johnAccounts;
                budgets[0].User = users[0];
                budgets[1].User = users[1];
                budgets[2].User = users[2];

                var ffdate = DateTime.UtcNow.AddDays(300);
                var goals = new List<Goal> {
                    new Goal
                    {
                        Name = "chuj",
                        EndDate = ffdate,
                        CurrentAmount = 234.21,
                        RequiredAmount = 222222.12,
                        BudgetId = budgets[0].Id
                    },
                    new Goal
                    {
                        Name = "chujec",
                        EndDate = ffdate.AddDays(325),
                        CurrentAmount = 1234.21,
                        RequiredAmount = 2112222.12,
                        BudgetId = budgets[0].Id
                    }
                };

                var fdate = DateTime.UtcNow.AddDays(20);
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
                        Frequency="Monthly",
                        Goal = goals[1],
                        Date = fdate.AddDays(5),
                        Budget=budgets[0]
                    },
                };

                var fTran = new List<FutureTransaction>
                {
                    new FutureTransaction
                    {
                        Category="Groceries",
                        Frequency="Weekly",
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
                        Frequency="Monthly",
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
                        Goal=goals[1]
                    },
                    new Saving
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(11),
                        Amount=200,
                        FromAccount=johnAccounts[1],
                        ToAccount=johnAccounts[2],
                        Goal=goals[0]
                    }
                };

                var tran = new List<Transaction>
                {
                    new Transaction
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(13),
                        Amount=50,
                        Title="Biedra",
                        Category="Groceries",
                        Account=johnAccounts[0],
                    },
                    new Transaction
                    {
                        Budget= budgets[0],
                        Date= date.AddDays(28),
                        Amount=50,
                        Title="Mefedron",
                        Category="Drugs",
                        Account=johnAccounts[1],
                    }
                };

                await context.Transactions.AddRangeAsync(tran);
                await context.Savings.AddRangeAsync(sav);
                await context.FutureTransactions.AddRangeAsync(fTran);
                await context.FutureSavings.AddRangeAsync(fSav);
                await context.Goals.AddRangeAsync(goals);
                await context.Accounts.AddRangeAsync(johnAccounts);
                await context.Budgets.AddRangeAsync(budgets);
                await context.SaveChangesAsync();
            }
        }
    }
}