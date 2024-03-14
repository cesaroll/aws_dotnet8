/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Domain.Models;
using Domain.Persistance;
using Persistance.DynamoDb.Items;
using Persistance.DynamoDb.Mappers;

namespace Persistance.DynamoDb.Repositories;

public class QueryRepository : IQueryRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;

    private readonly string _tableName = "customers";

    public QueryRepository(IAmazonDynamoDB dynamoDb)
    {
        _dynamoDb = dynamoDb;
    }

    public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var getItemRequest = new GetItemRequest {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue> {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };

        var response = await _dynamoDb.GetItemAsync(getItemRequest, cancellationToken);

        if (response.HttpStatusCode != HttpStatusCode.OK)
            throw new Exception("Error getting customer from DynamoDB");

        if (response.Item.Count == 0)
            return null;

        var itemAsDocument = Document.FromAttributeMap(response.Item);
        var customerItem = JsonSerializer.Deserialize<CustomerItem>(itemAsDocument.ToJson());

        return customerItem?.ToCustomer();
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var scanRequest = new ScanRequest {
            TableName = _tableName
        };

        var response = await _dynamoDb.ScanAsync(scanRequest, cancellationToken); // This is bad

        if (response.HttpStatusCode != HttpStatusCode.OK)
            throw new Exception("Error getting customers from DynamoDB");

        var customerItems = response.Items.Select(item => {
            var itemAsDocument = Document.FromAttributeMap(item);
            return JsonSerializer.Deserialize<CustomerItem>(itemAsDocument.ToJson())?.ToCustomer();
        })
        .Where(customer => customer != null)
        .Select(customer => customer!);

        return customerItems;
    }
}
