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
            if (!userManager.Users.Any() && !context.Budgets.Any())
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
                var johnFutureSavings = new List<FutureSavings>
                {
                };
                var johnFutureTransactions = new List<FutureTransaction>
                {
                };
                var johnSavings = new List<Savings>
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

                await context.Accounts.AddRangeAsync(johnAccounts);
                await context.Budgets.AddRangeAsync(budgets);
                await context.SaveChangesAsync();
            }
            if (!context.Goals.Any())
            {
                var date = DateTime.UtcNow.AddDays(300);
                var goals = new List<Goal> {
                    new Goal
                    {
                        Name = "chuj",
                        EndDate = date,
                        CurrentAmount = 234.21,
                        RequiredAmount = 222222.12,
                        BudgetId = Guid.Parse("58661662-D123-4249-8E82-184F8A4D1784")
                    },
                    new Goal
                    {
                        Name = "chujec",
                        EndDate = date,
                        CurrentAmount = 1234.21,
                        RequiredAmount = 2112222.12,
                        BudgetId = Guid.Parse("58661662-D123-4249-8E82-184F8A4D1784")
                    }
                };
                await context.Goals.AddRangeAsync(goals);
                await context.SaveChangesAsync();
            }
        }
    }
}