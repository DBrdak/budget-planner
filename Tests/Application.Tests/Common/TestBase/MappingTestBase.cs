using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Tests.Common.DataContextBase;
using Persistence;

namespace Application.Tests.Common.TestBase
{
    public class MappingTestBase
    {
        protected readonly IMapper _mapper;
        protected readonly DataContext _context;

        public MappingTestBase()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Core.MappingProfiles>();
            });

            _context = DataContextFactory.Create();
            _mapper = configurationProvider.CreateMapper();
        }
    }
}
