/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using Domain.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App;

public class GetByIdCustomerHandler : IRequestHandler<GetByIdCustomerQuery, Customer?>
{
    private readonly IRepository _repository;
    private readonly ILogger<GetByIdCustomerHandler> _logger;

    public GetByIdCustomerHandler(IRepository repository, ILogger<GetByIdCustomerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Customer?> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken) =>
        await _repository.GetCustomerAsync(request.Id, cancellationToken);
}
