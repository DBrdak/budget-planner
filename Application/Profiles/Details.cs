using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class Details
    {
        public class Query : IRequest<Result<ProfileDto>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<ProfileDto>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
                _budgetAccessor = budgetAccessor;
            }

            async Task<Result<ProfileDto>> IRequestHandler<Query, Result<ProfileDto>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var profile = await _context.Users
                    .AsNoTracking()
                    .ProjectTo<ProfileDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(u => u.Username == _userAccessor.GetUsername());

                if (profile == null)
                    return null;

                profile.BudgetName = _budgetAccessor.GetBudgetName();

                return Result<ProfileDto>.Success(profile);
            }
        }
    }
}