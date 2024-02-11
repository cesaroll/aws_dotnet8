/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Api.Errors;

public record ErrorDto()
{
    public string Name { get; init; } = "";
    public string Message { get; init; } = "";
}
