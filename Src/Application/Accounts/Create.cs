﻿using Application.Core;
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

namespace Application.Accounts
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AccountDto NewAccount { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IValidationExtension _validationExtension;

            public CommandValidator(IValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;

                RuleFor(x => x.NewAccount).SetValidator(new AccountValidator(_validationExtension));
            }
        }

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

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.NewAccount is null)
                    return null;

                var newAccount = _mapper.Map<Account>(request.NewAccount);

                var budgetId = await _budgetAccessor.GetBudgetId();

                newAccount.Budget = await _context.Budgets.FindAsync(budgetId);

                if (newAccount.Budget == null)
                    return null;

                await _context.Accounts.AddAsync(newAccount);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new account");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}