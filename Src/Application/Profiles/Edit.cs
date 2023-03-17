using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

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
            public CommandValidator(IProfileValidationExtension validationExtension)
            {
                RuleFor(x => x.NewProfile).SetValidator(new ProfileValidator(validationExtension));
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<User> _userManager;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor,
                UserManager<User> userManager, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
                _userManager = userManager;
                _budgetAccessor = budgetAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var oldProfile = await _userManager
                    .FindByNameAsync(_userAccessor.GetUsername())
                    .ConfigureAwait(false);

                if (oldProfile == null)
                    return null;

                _mapper.Map(request.NewProfile, oldProfile);

                var budgetName = await _budgetAccessor.GetBudget().ConfigureAwait(false);
                budgetName.Name = request.NewProfile.BudgetName;

                var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while updating new user data");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}