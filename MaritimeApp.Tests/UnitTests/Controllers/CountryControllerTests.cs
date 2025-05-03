using Xunit;
using Moq;
using server.Controllers;
using server.Dtos.Country;
using server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using server.Services.Interfaces;

namespace MaritimeApp.Tests.UnitTests.Controllers;

public class CountryControllerTests
{
    private readonly Mock<ICountryService> _mockService;
    private readonly CountryController _controller;

    public CountryControllerTests()
    {
        _mockService = new Mock<ICountryService>();
        _controller = new CountryController(_mockService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOkWithCountries()
    {
        var countries = new List<CountryResponseDto>
        {
            new CountryResponseDto(1, "Romania"),
            new CountryResponseDto(2, "Greece")
        };

        _mockService.Setup(s => s.GetAll()).Returns(countries);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCountries = Assert.IsAssignableFrom<IEnumerable<CountryResponseDto>>(okResult.Value);
        Assert.Equal(2, returnedCountries.Count());
    }

    [Fact]
    public void GetById_ExistingId_ReturnsOk()
    {
        var country = new CountryResponseDto(1, "Romania");
        _mockService.Setup(s => s.GetById(1)).Returns(country);

        var result = _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCountry = Assert.IsType<CountryResponseDto>(okResult.Value);
        Assert.Equal("Romania", returnedCountry.Name);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.GetById(99)).Returns((CountryResponseDto?)null);

        var result = _controller.GetById(99);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        var request = new CountryRequestDto("Spain");
        var created = new CountryResponseDto(3, "Spain");

        _mockService.Setup(s => s.Create(request)).Returns(created);

        var result = _controller.Create(request);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedCountry = Assert.IsType<CountryResponseDto>(createdAtActionResult.Value);
        Assert.Equal("Spain", returnedCountry.Name);
    }

    [Fact]
    public void Update_ExistingId_ReturnsOk()
    {
        var request = new CountryRequestDto("UpdatedName");
        var updated = new CountryResponseDto(1, "UpdatedName");

        _mockService.Setup(s => s.Update(1, request)).Returns(updated);

        var result = _controller.Update(1, request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCountry = Assert.IsType<CountryResponseDto>(okResult.Value);
        Assert.Equal("UpdatedName", returnedCountry.Name);
    }

    [Fact]
    public void Update_NonExistingId_ReturnsNotFound()
    {
        var request = new CountryRequestDto("GhostCountry");

        _mockService.Setup(s => s.Update(99, request)).Returns((CountryResponseDto?)null);

        var result = _controller.Update(99, request);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ExistingId_ReturnsOk()
    {
        var deleted = new CountryResponseDto(1, "ToDelete");

        _mockService.Setup(s => s.Delete(1)).Returns(deleted);

        var result = _controller.Delete(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCountry = Assert.IsType<CountryResponseDto>(okResult.Value);
        Assert.Equal("ToDelete", returnedCountry.Name);
    }

    [Fact]
    public void Delete_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.Delete(99)).Returns((CountryResponseDto?)null);

        var result = _controller.Delete(99);

        Assert.IsType<NotFoundResult>(result);
    }
}
