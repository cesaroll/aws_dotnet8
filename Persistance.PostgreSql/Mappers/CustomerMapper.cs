/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Domain.Models;
using Persistance.PostgreSql.Entities;
using Riok.Mapperly.Abstractions;

namespace Persistance.PostgreSql.Mappers;

public static class CustomerMapperExtensions
{
    public static CustomerEntity ToCustomerEntity(this Customer customer) =>
        CustomerMapper.Instance.MapCustomerToCustomerEntity(customer);

    public static Customer ToCustomer(this CustomerEntity customerEntity) =>
        CustomerMapper.Instance.CustomerEntityToCustomer(customerEntity);

    public static List<Customer> ToCustomers(this List<CustomerEntity> customerEntities) =>
        CustomerMapper.Instance.CustomerEntitiesToCustomers(customerEntities);
}

[Mapper]
public partial class CustomerMapper
{
    public static CustomerMapper Instance { get; } = new CustomerMapper();

    public CustomerEntity MapCustomerToCustomerEntity(Customer customer)
    {
        var entity = CustomerToCustomerEntity(customer);
        entity.DateOfBirth = entity.DateOfBirth.ToUniversalTime();
        return entity;
    }
    private partial CustomerEntity CustomerToCustomerEntity(Customer customer);
    public partial Customer CustomerEntityToCustomer(CustomerEntity customerEntity);
    public partial List<Customer> CustomerEntitiesToCustomers(List<CustomerEntity> customerEntities);
}

