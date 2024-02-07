/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
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
    private readonly ILogger<CustomerCommandHandler> _logger;

    public CustomerCommandHandler(IRepository repository, ILogger<CustomerCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Customer> Handle(CustomerCreateCommand request, CancellationToken cancellationToken) =>
        await _repository.AddCustomerAsync(request.NewCustomer, cancellationToken);

    public Task<Customer> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken) =>
        _repository.UpdateCustomerAsync(request.UpdatedCustomer, cancellationToken);

    public Task Handle(CustomerDeleteCommand request, CancellationToken cancellationToken) =>
        _repository.DeleteCustomerAsync(request.Id, cancellationToken);
}
