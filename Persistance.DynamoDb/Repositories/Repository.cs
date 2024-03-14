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

        var customerAsAttributesMap = customerItem.ToAttributeMap();

        var addItemRequest = new PutItemRequest {
            TableName = _tableName,
            Item = customerAsAttributesMap,
            ConditionExpression = "attribute_not_exists(pk)"
        };

        var response = await _dynamoDb.PutItemAsync(addItemRequest, cancellationToken);


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

        var customerAsAttributesMap = customerItem.ToAttributeMap();

        var updateItemRequest = new PutItemRequest {
            TableName = _tableName,
            Item = customerAsAttributesMap,
            ConditionExpression = "Version < :newVersion",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                { ":newVersion", new AttributeValue { N = customer.Version.ToString() } }
            }
        };

        try
        {
            var response = await _dynamoDb.PutItemAsync(updateItemRequest, cancellationToken);

            if(response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception("Error updating customer in DynamoDB");

            return customer;
        }
        catch (ConditionalCheckFailedException ex)
        {
            throw new VersionException("Version error, race condition", ex);
        }
    }
}
