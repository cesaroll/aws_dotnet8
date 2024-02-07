/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */

using Domain.Models;
using MediatR;

namespace App.Customers.Queries;

public record CustomersQuery : IRequest<IEnumerable<Customer>>
{
    public static readonly CustomersQuery Instance = new();

    private CustomersQuery() { }
}
