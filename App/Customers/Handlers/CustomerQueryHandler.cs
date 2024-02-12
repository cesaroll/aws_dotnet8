/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using App.Customers.Queries;
using Domain.Models;
using Domain.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App;

public class CustomerQueryHandler :
    IRequestHandler<CustomerQuery, Customer?>,
    IRequestHandler<CustomersQuery, IEnumerable<Customer>>
{
    private readonly IRepository _repository;
    private readonly ILogger<CustomerQueryHandler> _logger;

    public CustomerQueryHandler(IRepository repository, ILogger<CustomerQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Customer?> Handle(CustomerQuery request, CancellationToken cancellationToken) =>
        await _repository.GetCustomerAsync(request.Id, cancellationToken);

    public async Task<IEnumerable<Customer>> Handle(CustomersQuery request, CancellationToken cancellationToken) =>
        await _repository.GetCustomersAsync(cancellationToken);
}
