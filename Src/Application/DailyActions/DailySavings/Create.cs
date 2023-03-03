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
using System.Net.Http;

namespace Application.DailyActions.DailySavings
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public SavingDto NewSaving { get; set; }
        }

        //Place for validator function
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IMapper mapper, IHttpContextAccessor httpContext, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
                _budgetAccessor = budgetAccessor;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                // Tu też możemy zrezygnować z mappera na rzecz tradycyjnego tworzenia obiektu
                var newSaving = _mapper.Map<Saving>(request.NewSaving);

                // Obczaj nową funkcje w BudgetAccessor, większa wydajność
                var budgetName = await _budgetAccessor.GetBudgetName();

                // Jak wykorzystasz inną funkcję to masz Guid, więc możesz zmienić FirstOrDefault,
                // na coś "czystszego" (chodzi o konkretną metodę wbudowaną)
                newSaving.Budget = await _context.Budgets
                    .FirstOrDefaultAsync(b => b.Name == budgetName);

                newSaving.FromAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewSaving.FromAccountName
                    && a.Budget.Name == budgetName);

                newSaving.ToAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewSaving.ToAccountName
                    && a.Budget.Name == budgetName);

                newSaving.Goal = await _context.Goals
                    .FirstOrDefaultAsync(g => g.Name == request.NewSaving.GoalName
                    && g.Budget.Name == budgetName);

                // Nie masz wartości dla wszystkich property obiektu newSaving,
                // spójrz w Domain.Saving i zobacz czego brakuje

                // Spojler
                {
                    // Musisz znaleźć FutureSaving, nie ogarnąłem że może być ciężko bez info od clienta
                    // Więc wydaje mi się że najlepiej będzie wyszukać FutureSaving wg FromAccount i ToAccount,
                    // Natomiast jeżeli istnieje więcej niż jeden FutureSaving który ma dopasowane FromAccount i ToAccount
                    // to musisz dodać wyszukiwanie wg goal (bądź na odwrót, najpierw goal, potem from i to)
                    // Jeżeli użytkownik określił dwa takie same FutureSavings, no to jego problem, wtedy bierzemy ten
                    // Który nie jest w 100% spełniony (sprawdzasz czy Completed Amount >= Amount)
                }

                if (newSaving.Budget == null
                    || newSaving.ToAccount == null
                    || newSaving.FromAccount == null)
                    return null;

                // Dodatkowo trzeba zwiększyć completed amount w odpowiednim future saving i goal

                await _context.Savings.AddAsync(newSaving);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new saving");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}