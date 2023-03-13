using Application.Core;
using AutoMapper;
using Persistence;

namespace Application.Tests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly IMapper _mapper;
        protected readonly DataContext _context;

        public CommandTestBase()
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