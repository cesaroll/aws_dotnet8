/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Api.Dtos;

public record class CreateCustomerDto
{
    public string FullName { get; init; } = "";

    public string Email { get; init; } = "";

    public string GitHubUsername { get; init; } = "";

    public DateTime DateOfBirth { get; set; }
}
