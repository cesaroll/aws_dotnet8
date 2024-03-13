/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */

using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Consumer.SQS.Config;
using Domain.Messages;
using MediatR;
using Microsoft.Extensions.Options;

namespace Consumer.SQS.Services;

public class QueueConsumerService : BackgroundService
{
    private readonly IAmazonSQS _sqsClient;
    private readonly IOptions<QueueSettings> _queueSettings;
    private readonly IMediator _mediator;
    private readonly ILogger<QueueConsumerService> _logger;

    private string? _queueUrl;

    public QueueConsumerService(IAmazonSQS sqsClient, IOptions<QueueSettings> queueSettings, IMediator mediator, ILogger<QueueConsumerService> logger)
    {
        _sqsClient = sqsClient;
        _queueSettings = queueSettings;
        _mediator = mediator;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrl = await GetQueueUrlAsync(stoppingToken);

        var receiveMessageReguest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            MaxNumberOfMessages = 1,
            AttributeNames = new List<string> { "All" },
            MessageAttributeNames = new List<string> { "All" }
        };

        while(!stoppingToken.IsCancellationRequested)
        {
            var response = await _sqsClient.ReceiveMessageAsync(receiveMessageReguest, stoppingToken);

            foreach (var message in response.Messages)
            {
               try
               {
                    var messageType = message.MessageAttributes["MessageType"].StringValue;
                    var type = Type.GetType($"Domain.Messages.{messageType}, Domain");

                    if (type is null)
                    {
                        _logger.LogWarning("Invalid MessageType: {messageType}", messageType);
                        continue;
                    }

                    //_logger.Information("Message: {message}", message.Body);

                    var messageObject = JsonSerializer.Deserialize(message.Body, type, JsonSerializerOptions) as Domain.Messages.Message;

                    if(!IsValid(messageObject))
                    {
                        _logger.LogWarning("Invalid message: {message}", message.Body);
                        continue;
                    }

                    await _mediator.Send(messageObject!, stoppingToken);

                    await _sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle, stoppingToken);
               } catch(Exception ex)
               {
                     _logger.LogError(ex, "Error processing message: {@message}", message);
                     continue;
               }
            }

            _logger.LogInformation($"{_queueSettings.Value.Name} - {((char)9201).ToString()}");
            await Task.Delay(1000 * 3, stoppingToken);
        }
    }

    private async Task<string> GetQueueUrlAsync(CancellationToken cancellationToken)
    {
        if (_queueUrl is not null)
            return _queueUrl;

        var queueUrlResponse = await _sqsClient.GetQueueUrlAsync(_queueSettings.Value.Name, cancellationToken);
        _queueUrl = queueUrlResponse.QueueUrl;

        return _queueUrl;
    }

    private static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

    private bool IsValid(Domain.Messages.Message? message)
    {
        if (message is null)
            return false;

        if (message.Customer is null)
            return false;

        return true;
    }
}
