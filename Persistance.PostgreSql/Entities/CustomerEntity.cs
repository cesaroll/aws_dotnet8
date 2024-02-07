/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistance.PostgreSql.Entities;

[Table("Customers")]
public partial class CustomerEntity
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string GitHubUsername { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}

