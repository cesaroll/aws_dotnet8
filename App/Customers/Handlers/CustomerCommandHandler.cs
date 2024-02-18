/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using Domain.Models.EventModels;
using Domain.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App;

public class CustomerCommandHandler :
    IRequestHandler<CustomerCreateCommand, Customer>,
    IRequestHandler<CustomerUpdateCommand, Customer>,
    IRequestHandler<CustomerDeleteCommand>
{
    private readonly IRepository _repository;
    private readonly IMessenger _messenger;
    private readonly ILogger<CustomerCommandHandler> _logger;

    public CustomerCommandHandler(IRepository repository, ILogger<CustomerCommandHandler> logger, IMessenger messenger)
    {
        _repository = repository;
        _logger = logger;
        _messenger = messenger;
    }

    public async Task<Customer> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.AddCustomerAsync(request.NewCustomer, cancellationToken);
        await _messenger.SendMessageAsync(new CustomerCreated{
            PublishedAt = DateTime.UtcNow,
            Customer = customer
        }, cancellationToken);
        return customer;
    }

    public async Task<Customer> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.UpdateCustomerAsync(request.UpdatedCustomer, cancellationToken);
        await _messenger.SendMessageAsync(new CustomerUpdated{
            PublishedAt = DateTime.UtcNow,
            Customer = customer
        }, cancellationToken);
        return customer;
    }


    public async Task Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.DeleteCustomerAsync(request.Id, cancellationToken);

        if(customer is not null)
            await _messenger.SendMessageAsync(new CustomerDeleted{
                PublishedAt = DateTime.UtcNow,
                Customer = customer
            }, cancellationToken);
    }
}
