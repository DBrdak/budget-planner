using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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