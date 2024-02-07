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

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
{
    private readonly IRepository _repository;
    private readonly ILogger<GetAllCustomersHandler> _logger;

    public GetAllCustomersHandler(IRepository repository, ILogger<GetAllCustomersHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetCustomersAsync(cancellationToken);
    }
}
