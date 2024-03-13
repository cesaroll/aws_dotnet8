/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using System.Net;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Domain.Models;
using Domain.Persistance;
using Persistance.DynamoDb.Items;
using Persistance.DynamoDb.Mappers;

namespace Persistance.DynamoDb.Repositories;

public class Repository : IRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly string _tableName = "customers";

    public Repository(IAmazonDynamoDB dynamoDb)
    {
        _dynamoDb = dynamoDb;
    }

    public async Task<Customer> AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        var customerItem = customer.ToCustomerItem();
        customerItem.UpdatedAt = DateTime.UtcNow;

        var response = await PutCustomerAsync(customerItem, cancellationToken);

        if(response.HttpStatusCode != HttpStatusCode.OK)
            throw new Exception("Error adding customer to DynamoDB");

        return customer;
    }

    public async Task<Customer?> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deleteItemRequest = new DeleteItemRequest {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue> {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };

        var response = await _dynamoDb.DeleteItemAsync(deleteItemRequest, cancellationToken);

        if (response.HttpStatusCode != HttpStatusCode.OK)
            throw new Exception("Error deleting customer from DynamoDB");

        return null;
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        var customerItem = customer.ToCustomerItem();
        customerItem.UpdatedAt = DateTime.UtcNow;

        var response = await PutCustomerAsync(customerItem, cancellationToken);

        if(response.HttpStatusCode != HttpStatusCode.OK)
            throw new Exception("Error updating customer in DynamoDB");

        return customer;
    }

    private async Task<PutItemResponse> PutCustomerAsync(CustomerItem customerItem, CancellationToken cancellationToken = default)
    {
        var customerAsAttributesMap = customerItem.ToAttributeMap();

        var putItemRequest = new PutItemRequest {
            TableName = _tableName,
            Item = customerAsAttributesMap
        };

        return await _dynamoDb.PutItemAsync(putItemRequest, cancellationToken);
    }
}
