using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FutureSavings
{
    public class List
    {
        public class Query : IRequest<Result<List<FutureSavingDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<FutureSavingDto>>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            async Task<Result<List<FutureSavingDto>>> IRequestHandler<Query, Result<List<FutureSavingDto>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var futureSavings = await _context.FutureSavings
                    .AsNoTracking()
                    .Where(fs => fs.Budget.User.UserName == _userAccessor.GetUsername())
                    .ProjectTo<FutureSavingDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<FutureSavingDto>>.Success(futureSavings);
            }
        }
    }
}