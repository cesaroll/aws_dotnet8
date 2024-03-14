/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Persistance.DynamoDb;

public class VersionException : Exception
{
    public VersionException(string message, Exception innerException) : base(message, innerException) { }
}
