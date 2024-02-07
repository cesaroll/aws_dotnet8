/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using MediatR;

namespace App;

public record class CustomerUpdateCommand : IRequest<Customer>
{
    public Customer UpdatedCustomer { get; init; }
}
