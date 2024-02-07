/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using Domain.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
{
    private readonly IRepository _repository;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;

    public UpdateCustomerCommandHandler(IRepository repository, ILogger<UpdateCustomerCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken) =>
        _repository.UpdateCustomerAsync(request.UpdatedCustomer, cancellationToken);
}
