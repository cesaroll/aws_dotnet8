/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Amazon.SimpleNotificationService;
using Domain.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.SNS.Config;

public static class DIExtensions
{
    public static IServiceCollection AddSnsMessenger(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>();
        services.AddScoped<IMessenger, Messaging.Messenger>();

        return services;
    }
}
