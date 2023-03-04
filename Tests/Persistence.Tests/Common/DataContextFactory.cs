using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Tests.Common
{
    public class DataContextFactory
    {
        public static DataContext Create()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            SeedTestData.Seed(context);

            return context;
        }

        public static void Destroy(DataContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}