/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using MediatR;

namespace Domain.Messages;

public abstract class Message : IRequest
{
    public DateTime PublishedAt { get; set; }
    public Customer Customer { get; set; } = null!;
}
