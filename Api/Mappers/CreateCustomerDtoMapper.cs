/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Api.Dtos;
using Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Api.Mappers;

public static class CreateCustomerDtoMapperExtensions
{
    public static Customer ToCustomer(this CreateCustomerDto createCustomerDto) =>
        CreateCustomerDtoMapper.Instance.MapCreateCustomerDtoToCustomer(createCustomerDto);
}

[Mapper]
public partial class CreateCustomerDtoMapper
{
    public static CreateCustomerDtoMapper Instance { get; } = new CreateCustomerDtoMapper();

    public Customer MapCreateCustomerDtoToCustomer(CreateCustomerDto createCustomerDto)
    {
        var customer = CreateCustomerDtoToCustomer(createCustomerDto);
        customer.Id = Guid.NewGuid();
        return customer;
    }

    public partial Customer CreateCustomerDtoToCustomer(CreateCustomerDto createCustomerDto);
}
