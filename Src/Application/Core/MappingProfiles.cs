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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Get

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
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name))
                .ForMember(d => d.CompletedExpenditures, o => o.MapFrom(s => s.CompletedTransactions.Where(ct => ct.Amount < 0)));

            CreateMap<FutureTransaction, FutureIncomeDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name))
                .ForMember(d => d.CompletedIncomes, o => o.MapFrom(s => s.CompletedTransactions.Where(ct => ct.Amount > 0)));

            CreateMap<FutureSaving, FutureSavingDto>()
                .ForMember(d => d.FromAccountName, o => o.MapFrom(s => s.FromAccount.Name))
                .ForMember(d => d.ToAccountName, o => o.MapFrom(s => s.ToAccount.Name))
                .ForMember(d => d.GoalName, o => o.MapFrom(s => s.Goal.Name));

            CreateMap<Goal, GoalDto>();

            CreateMap<Account, AccountDto>()
                .ForMember(d => d.Expenditures, o => o.MapFrom(s => s.Transactions.Where(t => t.Amount < 0)))
                .ForMember(d => d.Incomes, o => o.MapFrom(s => s.Transactions.Where(t => t.Amount > 0)));

            CreateMap<User, ProfileDto>();

            CreateMap<TransactionCategory, TransactionCategoryDto>();

            //Set

            CreateMap<ExpenditureDto, Transaction>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => -s.Amount));

            CreateMap<FutureExpenditureDto, FutureTransaction>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => -s.Amount));

            CreateMap<FutureIncomeDto, FutureTransaction>();

            CreateMap<FutureSavingDto, FutureSaving>();

            CreateMap<GoalDto, Goal>()
                .ForMember(d => d.CurrentAmount, o => o.Ignore());

            CreateMap<AccountDto, Account>()
                .ForMember(d => d.Transactions, o => o.Ignore())
                .ForMember(d => d.SavingsIn, o => o.Ignore())
                .ForMember(d => d.SavingsOut, o => o.Ignore());

            CreateMap<ProfileDto, User>();
        }
    }
}