/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */

using Domain.Models;
using MediatR;

namespace App.Customers.Queries;

public record GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
{
    public static readonly GetAllCustomersQuery Instance = new();

    private GetAllCustomersQuery() { }
}
