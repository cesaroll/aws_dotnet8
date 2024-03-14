/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using MediatR;

namespace App.Customers.Commands;

public record class CustomerDeleteCommand : IRequest
{
    public Guid Id { get; init; }
}
