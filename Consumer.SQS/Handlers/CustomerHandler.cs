/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using System.Text.Json;
using Domain.Messages;
using MediatR;

namespace Consumer.SQS.Handlers;

public class CustomerHandler :
    IRequestHandler<CustomerCreated>,
    IRequestHandler<CustomerUpdated>,
    IRequestHandler<CustomerDeleted>
{

    private readonly ILogger<CustomerHandler> _logger;

    public CustomerHandler(ILogger<CustomerHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerCreated request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CustomerCreated: {request}", JsonSerializer.Serialize(request));
        return Task.CompletedTask;
    }

    public Task Handle(CustomerUpdated request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CustomerUpdated: {request}", JsonSerializer.Serialize(request));
        return Task.CompletedTask;
    }

    public Task Handle(CustomerDeleted request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CustomerDeleted: {request}", JsonSerializer.Serialize(request));
        return Task.CompletedTask;
    }
}
