/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using App.Customers.Queries;
using Microsoft.Extensions.DependencyInjection;
using Persistance.PostgreSql.Config;

namespace App;

public static class DIExtensions
{
    public static IServiceCollection AddApp(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining<CustomersQuery>();
        });
        return services;
    }
}
