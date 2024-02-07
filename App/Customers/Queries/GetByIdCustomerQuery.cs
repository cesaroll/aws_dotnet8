/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using MediatR;

namespace App;

public class GetByIdCustomerQuery : IRequest<Customer>
{
    public Guid Id { get; set; }
    public GetByIdCustomerQuery(Guid id)
    {
        Id = id;
    }
}
