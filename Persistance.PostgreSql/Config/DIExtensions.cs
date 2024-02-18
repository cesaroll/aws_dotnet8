/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Ardalis.GuardClauses;
using Domain.Persistance;
using Persistance.PostgreSql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance.PostgreSql.Config;

public static class DIExtensions
{
    public static IServiceCollection AddPostgresql(this IServiceCollection services, string connectionString)
    {
        var conn= Guard.Against.NullOrEmpty(connectionString, "PostgreSql connection string is required");

        services.AddDbContext<Context>(options => options.UseNpgsql(conn));
        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<Context>());

        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IQueryRepository, QueryRepository>();

        return services;
    }
}
