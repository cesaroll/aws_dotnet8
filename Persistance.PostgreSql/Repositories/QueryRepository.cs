/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using Domain.Persistance;
using Microsoft.EntityFrameworkCore;
using Persistance.PostgreSql.Mappers;

namespace Persistance.PostgreSql.Repositories;

public class QueryRepository : IQueryRepository
{
    private readonly IDbContext _dbContext;

    public QueryRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return customer?.ToCustomer();
    }

    public Task<Customer?> GetCustomerAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var customerEntities = await _dbContext.Customers.ToListAsync(cancellationToken);
        return customerEntities.ToCustomers();
    }
}
