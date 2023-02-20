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

namespace Application.SpendingPlan.Expenditures
{
    public class List
    {
        public class Query : IRequest<Result<List<FutureExpenditureDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<FutureExpenditureDto>>>
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

            async Task<Result<List<FutureExpenditureDto>>> IRequestHandler<Query, Result<List<FutureExpenditureDto>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var futureExpenditures = await _context.FutureTransactions
                    .AsNoTracking()
                    .Where(ft => ft.Budget.User.UserName == _userAccessor.GetUsername()
                        && ft.Amount < 0)
                    .ProjectTo<FutureExpenditureDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<FutureExpenditureDto>>.Success(futureExpenditures);
            }
        }
    }
}