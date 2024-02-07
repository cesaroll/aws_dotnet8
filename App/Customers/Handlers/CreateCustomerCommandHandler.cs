/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using Domain.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
{
    private readonly IRepository _repository;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;

    public CreateCustomerCommandHandler(IRepository repository, ILogger<CreateCustomerCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken) =>
        await _repository.AddCustomerAsync(request.NewCustomer, cancellationToken);
}
