/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using MediatR;

namespace App.Customers.Commands;

public record class CustomerCreateCommand : IRequest<Customer>
{
    public Customer NewCustomer { get; init; }
}
