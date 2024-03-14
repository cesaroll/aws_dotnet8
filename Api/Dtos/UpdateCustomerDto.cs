/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Api.Dtos;

public record class UpdateCustomerDto
{
    public Guid Id { get; set; }

    public int Version { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string GitHubUsername { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}
