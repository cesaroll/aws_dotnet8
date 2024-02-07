/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using MediatR;

namespace App;

public record class DeleteCustomerCommand : IRequest
{
    public Guid Id { get; init; }
}
