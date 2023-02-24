﻿using Domain;
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