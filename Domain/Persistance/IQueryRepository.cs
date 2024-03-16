/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
 using Domain.Models;

namespace Domain.Persistance;

public interface IQueryRepository
{
    Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default);
}
