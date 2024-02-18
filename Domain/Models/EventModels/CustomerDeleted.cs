/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Domain.Models.EventModels;

public class CustomerDeleted
{
    public DateTime PublishedAt { get; set; }
    public Customer Customer { get; set; } = null!;
}
