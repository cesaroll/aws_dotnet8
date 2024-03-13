/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Amazon.DynamoDBv2;
using Domain.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Persistance.DynamoDb.Repositories;

namespace Persistance.DynamoDb.Config;

public static class DIExtensions
{
    public static IServiceCollection AddDynamoDb(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();

        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IQueryRepository, QueryRepository>();

        return services;
    }
}
