/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using App.Customers.Queries;
using Domain.Models;
using Domain.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Customers.Handlers;

public class CustomerQueryHandler :
    IRequestHandler<CustomerQuery, Customer?>,
    IRequestHandler<CustomersQuery, IEnumerable<Customer>>
{
    private readonly IQueryRepository _queryRepository;
    private readonly ILogger<CustomerQueryHandler> _logger;

    public CustomerQueryHandler(IQueryRepository queryRepository, ILogger<CustomerQueryHandler> logger)
    {
        _queryRepository = queryRepository;
        _logger = logger;
    }

    public async Task<Customer?> Handle(CustomerQuery request, CancellationToken cancellationToken) =>
        request.Id is not null ?
            await _queryRepository.GetCustomerAsync(request.Id.Value, cancellationToken) :
            await _queryRepository.GetCustomerAsync(request.Email!, cancellationToken);

    public async Task<IEnumerable<Customer>> Handle(CustomersQuery request, CancellationToken cancellationToken) =>
        await _queryRepository.GetCustomersAsync(cancellationToken);
}
