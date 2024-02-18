/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using Domain.Persistance;
using Microsoft.EntityFrameworkCore;
using Persistance.PostgreSql.Mappers;

namespace Persistance.PostgreSql.Repositories;

public class Repository : IRepository
{
    private readonly IDbContext _dbContext;

    public Repository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer> AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        var customerEntity = await _dbContext.Customers.AddAsync(customer.ToCustomerEntity(), cancellationToken);
        await _dbContext.SaveChangesAsync();
        return customerEntity.Entity.ToCustomer();
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        var customerEntity = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == customer.Id, cancellationToken);

        if (customerEntity == null)
            throw new KeyNotFoundException($"Customer with id {customer.Id} not found");

        customerEntity.FullName = customer.FullName;
        customerEntity.Email = customer.Email;
        customerEntity.GitHubUsername = customer.GitHubUsername;
        customerEntity.DateOfBirth = customer.DateOfBirth.ToUniversalTime();

        _dbContext.Customers.Update(customerEntity);
        await _dbContext.SaveChangesAsync();

        return customerEntity.ToCustomer();
    }

    public async Task<Customer?> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customerEntity = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (customerEntity == null)
            return null;

        _dbContext.Customers.Remove(customerEntity);
        await _dbContext.SaveChangesAsync();

        return customerEntity.ToCustomer();
    }
}
