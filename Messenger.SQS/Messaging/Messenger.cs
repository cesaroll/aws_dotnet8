/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Domain.Persistance;
using Messenger.SQS.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Messenger.SQS.Messaging;

public class Messenger : IMessenger
{
    private readonly IAmazonSQS _sqsClient;
    private readonly IOptions<QueueSettings> _queueSettings;
    private readonly ILogger<Messenger> _logger;

    private string? _queueUrl;

    public Messenger(IAmazonSQS sqsClient, IOptions<QueueSettings> queueSettings, ILogger<Messenger> logger)
    {
        _sqsClient = sqsClient;
        _queueSettings = queueSettings;
        _logger = logger;
    }

    public async Task SendMessageAsync<T>(T message, string command, CancellationToken cancellationToken = default)
    {
        var queueUrl = await GetQueueUrlAsync(cancellationToken);

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType",
                    new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                },
                {
                    "MessageCommand",
                    new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = command
                    }
                }
            }
        };

        var response = await _sqsClient.SendMessageAsync(sendMessageRequest, cancellationToken);

        _logger.LogInformation("Message sent to queue {QueueUrl} with message id {MessageId}", queueUrl, response.MessageId);
    }

    private async Task<string> GetQueueUrlAsync(CancellationToken cancellationToken)
    {
        if (_queueUrl is not null)
            return _queueUrl;

        var queueUrlResponse = await _sqsClient.GetQueueUrlAsync(_queueSettings.Value.Name, cancellationToken);
        _queueUrl = queueUrlResponse.QueueUrl;

        return _queueUrl;
    }
}
