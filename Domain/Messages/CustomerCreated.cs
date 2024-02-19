﻿/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;

namespace Domain.Messages;

public class CustomerCreated : IMessage
{
    public DateTime PublishedAt { get; set; }
    public Customer Customer { get; set; } = null!;
}