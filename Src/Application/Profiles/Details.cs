using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Profiles
{
    public class Details
    {
        public class Query : IRequest<Result<ProfileDto>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<ProfileDto>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(IUserAccessor userAccessor, IMapper mapper, UserManager<User> userManager, IBudgetAccessor budgetAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _userManager = userManager;
                _budgetAccessor = budgetAccessor;
            }

            public async Task<Result<ProfileDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var profile = _mapper.Map<ProfileDto>(await _userManager
                    .FindByNameAsync(_userAccessor.GetUsername())
                    .ConfigureAwait(true), opt => opt.Items["BudgetName"] = _budgetAccessor.GetBudgetName().Result);

                if (profile == null)
                    return null;

                return Result<ProfileDto>.Success(profile);
            }
        }
    }
}