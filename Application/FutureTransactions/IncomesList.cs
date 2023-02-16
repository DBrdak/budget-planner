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

namespace Application.FutureTransactions
{
    public class IncomesList
    {
        public class Query : IRequest<Result<List<FutureIncomeDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<FutureIncomeDto>>>
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

            async Task<Result<List<FutureIncomeDto>>> IRequestHandler<Query, Result<List<FutureIncomeDto>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var futureTransactions = await _context.FutureTransactions
                    .AsNoTracking()
                    .Where(ft => ft.Budget.User.UserName == _userAccessor.GetUsername())
                    .ProjectTo<FutureIncomeDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<FutureIncomeDto>>.Success(futureTransactions);
            }
        }
    }
}