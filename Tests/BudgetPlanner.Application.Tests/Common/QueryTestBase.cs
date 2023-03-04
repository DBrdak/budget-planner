using Application.Core;
using AutoMapper;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Common
{
    public class QueryTestBase : IDisposable
    {
        protected readonly IMapper _mapper;
        protected readonly DataContext _context;

        public QueryTestBase()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();
            _context = DataContextFactory.Create();
        }

        public void Dispose()
        {
            DataContextFactory.Destroy(_context);
        }
    }
}