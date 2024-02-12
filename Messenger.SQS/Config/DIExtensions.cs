/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Amazon.SQS;
using Domain.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.SQS.Config;

public static class DIExtensions
{
    public static IServiceCollection AddSqsMessenger(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
        services.AddScoped<IMessenger, Messaging.Messenger>();

        return services;
    }
}
