﻿using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tests.Common.DataContextBase;

public static class DataContextFactory
{
    public static DataContext Create()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);

        context.Seed();

        return context;
    }

    public static void Destroy(DataContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}