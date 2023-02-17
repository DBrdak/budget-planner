using Application.DTO;
using AutoMapper;
using Domain;
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
            CreateMap<Transaction, ExpenditureDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name))
                .ForAllMembers(d => d.Condition(t => t.Amount < 0));

            CreateMap<Transaction, IncomeDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name))
                .ForAllMembers(d => d.Condition(t => t.Amount > 0));

            CreateMap<FutureTransaction, FutureExpenditureDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name));

            CreateMap<FutureTransaction, FutureIncomeDto>()
                .ForMember(d => d.AccountName, o => o.MapFrom(s => s.Account.Name));

            CreateMap<Saving, SavingDto>()
                .ForMember(d => d.FromAccountName, o => o.MapFrom(s => s.FromAccount.Name))
                .ForMember(d => d.ToAccountName, o => o.MapFrom(s => s.ToAccount.Name))
                .ForMember(d => d.GoalName, o => o.MapFrom(s => s.Goal.Name));

            CreateMap<FutureSaving, FutureSavingDto>()
                .ForMember(d => d.FromAccountName, o => o.MapFrom(s => s.FromAccount.Name))
                .ForMember(d => d.ToAccountName, o => o.MapFrom(s => s.ToAccount.Name))
                .ForMember(d => d.GoalName, o => o.MapFrom(s => s.Goal.Name));

            CreateMap<Goal, GoalDto>();

            CreateMap<Account, AccountDto>()
                .ForMember(d => d.Expenditures, o => o.MapFrom(s => s.Transactions.Where(t => t.Amount < 0)))
                .ForMember(d => d.Incomes, o => o.MapFrom(s => s.Transactions.Where(t => t.Amount > 0)));
        }
    }
}