/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
 using Api.Dtos;
 using Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Api.Mappers;

public static class UpdateCustomerDtoMapperExtensions
{
    public static Customer ToCustomer(this UpdateCustomerDto updateCustomerDto, Guid id) =>
        UpdateCustomerDtoMapper.Instance.MapUpdateCustomerDtoToCustomer(updateCustomerDto, id);
}

[Mapper]
public partial class UpdateCustomerDtoMapper
{
    public static UpdateCustomerDtoMapper Instance { get; } = new UpdateCustomerDtoMapper();

    public Customer MapUpdateCustomerDtoToCustomer(UpdateCustomerDto updateCustomerDto, Guid id)
    {
        var customer = UpdateCustomerDtoToCustomer(updateCustomerDto);
        customer.Id = id;
        return customer;
    }

    public partial Customer UpdateCustomerDtoToCustomer(UpdateCustomerDto updateCustomerDto);
}
