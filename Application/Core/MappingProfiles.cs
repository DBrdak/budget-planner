using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            DataContext context = null;
            IBudgetAccessor budgetAccessor = null;

            CreateMap<Transaction, ExpenditureDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name))
                .ForAllMembers(d => d.Condition(t => t.Amount < 0));

            CreateMap<Transaction, IncomeDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name))
                .ForAllMembers(d => d.Condition(t => t.Amount > 0));

            CreateMap<Saving, SavingDto>()
                .ForMember(d => d.FromAccountName, o => o.MapFrom(s => s.FromAccount.Name))
                .ForMember(d => d.ToAccountName, o => o.MapFrom(s => s.ToAccount.Name))
                .ForMember(d => d.GoalName, o => o.MapFrom(s => s.Goal.Name));

            CreateMap<FutureTransaction, FutureExpenditureDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name));

            CreateMap<FutureExpenditureDto, FutureTransaction>();
            //.ForMember(d => d.Account, o => o.MapFrom(s => context.Accounts
            //    .Where(a => a.Budget.Name == budgetAccessor.GetBudgetName())
            //    .FirstOrDefault(a => a.Name == s.AccountName)));

            CreateMap<FutureTransaction, FutureIncomeDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name));

            CreateMap<FutureIncomeDto, FutureTransaction>();
            //.ForMember(d => d.Account, o => o.MapFrom(s => context.Accounts
            //    .Where(a => a.Budget.Name == budgetAccessor.GetBudgetName())
            //    .FirstOrDefault(a => a.Name == s.AccountName)));

            CreateMap<FutureSaving, FutureSavingDto>()
                .ForMember(d => d.FromAccountName, o => o.MapFrom(s => s.FromAccount.Name))
                .ForMember(d => d.ToAccountName, o => o.MapFrom(s => s.ToAccount.Name))
                .ForMember(d => d.GoalName, o => o.MapFrom(s => s.Goal.Name));

            CreateMap<FutureSavingDto, FutureSaving>();
            //.ForMember(d => d.FromAccount, o => o.MapFrom(s => context.Accounts
            //    .Where(a => a.Budget.Name == budgetAccessor.GetBudgetName())
            //    .FirstOrDefault(a => a.Name == s.FromAccountName)))
            //.ForMember(d => d.ToAccount, o => o.MapFrom(s => context.Accounts
            //    .Where(a => a.Budget.Name == budgetAccessor.GetBudgetName())
            //    .FirstOrDefault(a => a.Name == s.ToAccountName)))
            //.ForMember(d => d.Goal, o => o.MapFrom(s => context.Goals
            //    .Where(g => g.Budget.Name == budgetAccessor.GetBudgetName())
            //    .FirstOrDefault(g => g.Name == s.GoalName)));

            CreateMap<Goal, GoalDto>();

            CreateMap<GoalDto, Goal>()
                .ForMember(d => d.CurrentAmount, o => o.Ignore());

            CreateMap<Account, AccountDto>()
                .ForMember(d => d.Expenditures, o => o.MapFrom(s => s.Transactions.Where(t => t.Amount < 0)))
                .ForMember(d => d.Incomes, o => o.MapFrom(s => s.Transactions.Where(t => t.Amount > 0)));

            CreateMap<AccountDto, Account>()
                .ForMember(d => d.Transactions, o => o.Ignore())
                .ForMember(d => d.SavingsIn, o => o.Ignore())
                .ForMember(d => d.SavingsOut, o => o.Ignore());
        }
    }
}