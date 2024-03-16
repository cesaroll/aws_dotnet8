/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using MediatR;

namespace App.Customers.Queries;

public record CustomerQuery : IRequest<Customer>
{
    public Guid? Id { get; set; }
    public string? Email {get; set;}
    public CustomerQuery(string idOrEmail)
    {
        if(Guid.TryParse(idOrEmail, out var id))
            Id = id;
        else
            Email = idOrEmail;
    }
}
