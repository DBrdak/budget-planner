using System.Reflection;
using Application.Accounts;
using Application.Core;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using Infrastructure.Validation;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

/// <summary>
/// Adding all required services to IServiceCollection of API
/// </summary>

public static class AplicationServiceExtension
{
    public static IServiceCollection AddAppCollection(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(
                config.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000");
            });
        });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(List.Handler))));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Create>();
        services.AddHttpContextAccessor();

        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IBudgetAccessor, BudgetAccessor>();
        services.AddScoped<IValidationExtension, ValidationExtension>();
        services.AddScoped<IProfileValidationExtension, ProfileValidationExtension>();

        return services;
    }
}