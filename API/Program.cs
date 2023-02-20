using API.Extensions;
using API.Middleware;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAppCollection(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("CorsPolicy");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<DataContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            await context.Database.MigrateAsync();

            if (app.Environment.IsDevelopment())
            {
                await Seed.SeedData(context, userManager);
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex.Message, "Error occured during migration");
        }

        await app.RunAsync();
    }
}