using Application.Core;
using Application.Interfaces;
using Application.DTO;
using Domain;
using MediatR;
using Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Application.DailyActions.DailyExpenditures;
using FluentValidation;

namespace Application.DailyActions.DailyIncomes
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public IncomeDto NewIncome { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IValidationExtension _validationExtension;

            public CommandValidator(IValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;

                RuleFor(x => x.NewIncome).SetValidator(new IncomeValidator(_validationExtension));
            }
        }
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
                var newIncome = _mapper.Map<Transaction>(request.NewIncome);

                var budgetId = await _budgetAccessor.GetBudgetId();

                newIncome.BudgetId = budgetId;

                newIncome.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewIncome.AccountName
                        && a.BudgetId== budgetId);

                newIncome.FutureTransaction = await _context.FutureTransactions
                    .FirstOrDefaultAsync(ft => ft.Category == request.NewIncome.Category
                        && ft.BudgetId == budgetId);

                await _context.Transactions.AddAsync(newIncome);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new income");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}