using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Backend.Api.Controllers;
using Million.RealEstate.Backend.Application.DTOs;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Core.DTOs;
using Moq;

namespace Million.RealEstate.Backend.Tests;

[TestFixture]
public class PropertyControllerTests
{
    private Mock<IPropertyService> _mockPropertyService;
    private PropertyController _controller;

    [SetUp]
    public void Setup()
    {
        _mockPropertyService = new Mock<IPropertyService>();
        _controller = new PropertyController(_mockPropertyService.Object);
    }

    [Test]
    public async Task GetFiltered_WithValidFilters_ReturnsOkResult()
    {
        // Arrange
        var filter = new PropertyFilterDto { Page = 1, Limit = 10 };
        var expectedResult = new PagedResultDto<PropertySummaryDto>
        {
            Items = new List<PropertySummaryDto>
        {
            new PropertySummaryDto
            {
                Id = "1",
                IdOwner = "owner1",
                Name = "Casa bonita",
                Address = "Calle 123",
                Price = 100000,
                ImageUrl = "test.jpg"
            }
        },
            TotalCount = 1,
            TotalPages = 1,
            CurrentPage = 1,
            PageSize = 10
        };

        _mockPropertyService
            .Setup(service => service.GetFilteredAsync(It.IsAny<PropertyFilterDto>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.GetFiltered(filter);

        // Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);

        var value = okResult!.Value as PagedResultDto<PropertySummaryDto>;
        Assert.That(value, Is.Not.Null);

        // Validamos propiedades clave
        Assert.That(value!.TotalCount, Is.EqualTo(expectedResult.TotalCount));
        Assert.That(value.TotalPages, Is.EqualTo(expectedResult.TotalPages));
        Assert.That(value.CurrentPage, Is.EqualTo(expectedResult.CurrentPage));
        Assert.That(value.PageSize, Is.EqualTo(expectedResult.PageSize));
        Assert.That(value.Items.Count, Is.EqualTo(expectedResult.Items.Count));
        Assert.That(value.Items[0].Name, Is.EqualTo(expectedResult.Items[0].Name));
    }

    [Test]
    public async Task GetById_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = "nonexisting";

        _mockPropertyService
            .Setup(service => service.GetByIdAsync(nonExistingId))
            .ReturnsAsync((PropertySummaryDto)null);

        // Act
        var result = await _controller.GetByIdAsync(nonExistingId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
}
