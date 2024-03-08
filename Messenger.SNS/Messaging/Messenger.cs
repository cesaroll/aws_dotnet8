/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Domain.Persistance;
using Messenger.SNS.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Messenger.SNS.Messaging;

public class Messenger : IMessenger
{
    private readonly IAmazonSimpleNotificationService _snsClient;
    private readonly IOptions<SnsSettings> _snsSettings;
    private readonly ILogger<Messenger> _logger;

    private string? _topicArn;

    public Messenger(IAmazonSimpleNotificationService snsClient, IOptions<SnsSettings> snsSettings, ILogger<Messenger> logger)
    {
        _snsClient = snsClient;
        _snsSettings = snsSettings;
        _logger = logger;
    }

    public async Task SendMessageAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        var topicArn = await GetTopicArnAsync(cancellationToken);

        var publishRequest = new PublishRequest
        {
            TopicArn = topicArn,
            Message = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType",
                    new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        var response = _snsClient.PublishAsync(publishRequest, cancellationToken);

        _logger.LogInformation("Message published to {TopicArn} with message id {MessageId}", topicArn, response.Result.MessageId);
    }

    private async Task<string> GetTopicArnAsync(CancellationToken cancellationToken)
    {
        if(_topicArn is not null)
            return _topicArn;

        var response = await _snsClient.FindTopicAsync(_snsSettings.Value.Name);
        var topicArn = response.TopicArn;

        _topicArn = topicArn;

        return topicArn;
    }
}
