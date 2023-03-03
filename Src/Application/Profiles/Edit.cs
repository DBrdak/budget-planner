using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProfileDto NewProfile { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IProfileValidationExtension _validationExtension;

            public CommandValidator(IProfileValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;

                RuleFor(x => x.NewProfile).SetValidator(new ProfileValidator(_validationExtension));
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
                _budgetAccessor = budgetAccessor;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var oldProfile = await _context.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());

                if (oldProfile == null)
                    return null;

                _mapper.Map(request.NewProfile, oldProfile);

                var budgetName = await _budgetAccessor.GetBudgetName();

                var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.Name == budgetName);

                budget.Name = request.NewProfile.BudgetName;

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while updating new user data");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}