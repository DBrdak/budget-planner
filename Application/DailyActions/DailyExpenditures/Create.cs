using Application.Core;
using Application.Interfaces;
using Application.DTO;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Persistence.Migrations;
using System.Net.Http;

namespace Application.DailyActions.DailyExpenditures
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ExpenditureDto NewExpenditure { get; set; }
        }

        //Place for validator

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IMapper mapper, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _mapper = mapper;
                _budgetAccessor = budgetAccessor;
            }

            // Podpowiedzi odnoszą się też do create income
            // Do walidatorów nie chce mi się już pisać, bo za 5h wstaję, więc pisz jak coś
            // Tam będzie dużo roboty, będzie trzeba też wstrzyknąć zależność do IValidationExtension

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                // Nie mapujemy, zbyt duzy koszt wydajności, zamiast tego spróbuj tradycyjnie stworzyć nowy obiekt typu Transaction
                var newExpenditure = _mapper.Map<Transaction>(request.NewExpenditure);

                var budgetName = await _budgetAccessor.GetBudgetName();

                newExpenditure.Budget = await _context.Budgets
                    .FirstOrDefaultAsync(b => b.Name == budgetName);

                newExpenditure.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewExpenditure.AccountName
                    && a.Budget.Name == budgetName);

                // Brakuje tu ustalenia wartości dla FutureTransaction (patrz Domain.Transaction)
                // W _context musisz znaleźć taki obiekt FutureTransaction,
                // który będzie miał taką samą kategorię jak tworzone Transaction
                // w kodzie nie znajdziesz podpowiedzi, pamiętaj o await oraz szukaniu kategorii tylko dla aktualnego budżetu,
                // Możesz wydzierżawić funkcję szukania kategorii do interfejsu w Interfaces (musiałbyś stworzyć nowy)
                // potem implementując go w Infrastructure

                if (newExpenditure.Budget == null
                    || newExpenditure.Account == null)
                    return null;

                // Kategorię tworzymy tylko w spending planie
                // Kategoria to coś co użytkownik tworzy tylko i wyłącznie w spending planie
                // potem w momencie gdy dodaje daily actions musi sprecyzować do jakiej kategorii utworzonej wcześniej
                // chce przypisać nowy wydatek/ zarobek
                var category = new TransactionCategory
                {
                    Value = newExpenditure.Category,
                    Budget = await _budgetAccessor.GetBudget(),
                    Type = "expenditure"
                };

                // Musisz dodać do completed amount odpowiedniego future transaction wartość amount

                await _context.Transactions.AddAsync(newExpenditure);
                // Wypierdol
                await _context.TransactionCategories.AddAsync(category);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new expenditure");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}