/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistance.PostgreSql.Entities;

namespace Persistance.PostgreSql;

public interface IDbContext
{
    DbSet<CustomerEntity> Customers { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
