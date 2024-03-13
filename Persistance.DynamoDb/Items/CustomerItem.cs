/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using System.Text.Json.Serialization;

namespace Persistance.DynamoDb.Items;

public class CustomerItem
{
    [JsonPropertyName("pk")]
    public string Pk { get; set; } = null!;

    [JsonPropertyName("sk")]
    public string Sk { get; set; } = null!;
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string GitHubUsername { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public DateTime UpdatedAt { get; set; }
}
