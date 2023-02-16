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

namespace Application.Accounts
{
    public class List
    {
        public class Query : IRequest<Result<List<AccountDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<AccountDto>>>
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

            async Task<Result<List<AccountDto>>> IRequestHandler<Query, Result<List<AccountDto>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var accounts = await _context.Accounts
                    .AsNoTracking()
                    .Where(a => a.Budget.User.UserName == _userAccessor.GetUsername())
                    .ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<AccountDto>>.Success(accounts);
            }
        }
    }
}