/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IRepository _repository;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;

    public DeleteCustomerCommandHandler(IRepository repository, ILogger<DeleteCustomerCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken) =>
        _repository.DeleteCustomerAsync(request.Id, cancellationToken);
}
