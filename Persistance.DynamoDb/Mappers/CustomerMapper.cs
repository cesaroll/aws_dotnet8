/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Riok.Mapperly.Abstractions;
using Domain.Models;
using Persistance.DynamoDb.Items;
using Amazon.DynamoDBv2.Model;
using System.Text.Json;
using Amazon.DynamoDBv2.DocumentModel;

namespace Persistance.DynamoDb.Mappers;

public static class CustomerMapperExtensions
{
    public static CustomerItem ToCustomerItem(this Customer customer) =>
        CustomerMapper.Instance.MapCustomerToCustomerItem(customer);

    public static Customer ToCustomer(this CustomerItem customerItem) =>
        CustomerMapper.Instance.CustomerItemToCustomer(customerItem);

    public static List<Customer> ToCustomers(this List<CustomerItem> customerItems) =>
        CustomerMapper.Instance.CustomerItemsToCustomers(customerItems);

    public static Dictionary<string, AttributeValue> ToAttributeMap(this CustomerItem customerItem) =>
        Document.FromJson(JsonSerializer.Serialize(customerItem)).ToAttributeMap();
}

[Mapper]
public partial class CustomerMapper
{
    public static CustomerMapper Instance { get; } = new CustomerMapper();

    public CustomerItem MapCustomerToCustomerItem(Customer customer)
    {
        var entity = CustomerToCustomerItem(customer);
        entity.DateOfBirth = entity.DateOfBirth.ToUniversalTime();
        entity.Pk = customer.Id.ToString();
        entity.Sk = customer.Id.ToString();
        return entity;
    }
    private partial CustomerItem CustomerToCustomerItem(Customer customer);
    public partial Customer CustomerItemToCustomer(CustomerItem customerEntity);
    public partial List<Customer> CustomerItemsToCustomers(List<CustomerItem> customerEntities);
}

