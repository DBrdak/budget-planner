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
                var users = new List<User>
                {
                    new User
                    {
                        DisplayName="John",
                        UserName="john",
                        Email="john@test.com"
                    },
                    new User
                    {
                        DisplayName="Bob",
                        UserName="bob",
                        Email="bob@test.com"
                    },
                    new User
                    {
                        DisplayName="Jane",
                        UserName="jane",
                        Email="jane@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

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
                        Name="John's budget",
                        User=users[0],
                    },
                    new Budget
                    {
                        Name="Bob's budget",
                        User=users[1],
                    },
                    new Budget
                    {
                        Name="Jane's budget",
                        User=users[2],
                    }
                };
                var accounts = new List<Account>();

                await context.Accounts.AddRangeAsync(johnAccounts);
                await context.Budgets.AddRangeAsync(budgets);
                await context.SaveChangesAsync();
            }
        }
    }
}