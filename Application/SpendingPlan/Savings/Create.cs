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

namespace Application.SpendingPlan.Savings
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public FutureSavingDto NewFutureSaving { get; set; }
        }

        //public class CommandValidator : AbstractValidator<Command>
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
                var newFutureSaving = _mapper.Map<FutureSaving>(request.NewFutureSaving);

                var budgetName = _budgetAccessor.GetBudgetName();

                newFutureSaving.Budget = await _context.Budgets
                    .FirstOrDefaultAsync(b => b.Name == budgetName);

                if (newFutureSaving.Budget == null)
                    return null;

                await _context.FutureTransactions.AddAsync(newFutureSaving);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new income");

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}
