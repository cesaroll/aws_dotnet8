/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using AutoFixture;
using Domain.Models;
using Domain.Persistance;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace App.Tests.Unit;

public class CustomerQueryHandlerTests
{
    private CustomerQueryHandler _sut;
    private IQueryRepository _repository;
    private ILogger<CustomerQueryHandler> _logger;
    private IFixture _fixture;

    public CustomerQueryHandlerTests()
    {
        _repository = Substitute.For<IQueryRepository>();
        _logger = Substitute.For<ILogger<CustomerQueryHandler>>();

        _sut = new CustomerQueryHandler(_repository, _logger);

        _fixture = new Fixture();
    }

    [Fact]
    public  async Task CustomerQuery_Handle_ShouldReturn_SingleCustomer()
    {
        // Arrange
        var customer = _fixture.Build<Customer>()
            .With(c => c.Id, Guid.NewGuid())
            .Create();

        var query = new CustomerQuery(customer.Id);

        _repository.GetCustomerAsync(customer.Id, Arg.Any<CancellationToken>())
            .Returns(customer);

        // Act
        var result = await _sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(customer);
    }
}
