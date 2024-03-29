﻿/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Domain.Persistance;

public interface IMessenger
{
    Task SendMessageAsync<T>(T message, CancellationToken cancellationToken = default);
}
