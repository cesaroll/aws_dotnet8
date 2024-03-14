/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using MediatR;

namespace App.Customers.Queries;

public record CustomerQuery : IRequest<Customer>
{
    public Guid Id { get; set; }
    public CustomerQuery(Guid id)
    {
        Id = id;
    }
}
