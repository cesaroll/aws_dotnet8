/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;

namespace Domain.Persistance;

public interface IRepository
{
    Task<Customer> AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<Customer> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<Customer?> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default);
}
