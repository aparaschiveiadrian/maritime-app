using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using server.Controllers;
using server.Dtos.Ship;
using server.Services;
using System.Collections.Generic;
using System.Linq;
using server.Services.Interfaces;

namespace MaritimeApp.Tests.UnitTests.Controllers;

public class ShipControllerTests
{
    private readonly Mock<IShipService> _mockService;
    private readonly ShipController _controller;

    public ShipControllerTests()
    {
        _mockService = new Mock<IShipService>();
        _controller = new ShipController(_mockService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOkWithShips()
    {
        var ships = new List<ShipResponseDto>
        {
            new ShipResponseDto(1, "Titanic", 40),
            new ShipResponseDto(2, "Poseidon", 35)
        };

        _mockService.Setup(s => s.GetAll()).Returns(ships);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsAssignableFrom<IEnumerable<ShipResponseDto>>(okResult.Value);
        Assert.Equal(2, returned.Count());
    }

    [Fact]
    public void GetById_ExistingId_ReturnsOk()
    {
        var ship = new ShipResponseDto(1, "Titanic", 40);

        _mockService.Setup(s => s.GetById(1)).Returns(ship);

        var result = _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<ShipResponseDto>(okResult.Value);
        Assert.Equal("Titanic", returned.Name);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.GetById(999)).Returns((ShipResponseDto?)null);

        var result = _controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        var request = new ShipRequestDto("NewShip", 50);
        var created = new ShipResponseDto(3, "NewShip", 50);

        _mockService.Setup(s => s.Create(request)).Returns(created);

        var result = _controller.Create(request);

        var createdAt = Assert.IsType<CreatedAtActionResult>(result);
        var returned = Assert.IsType<ShipResponseDto>(createdAt.Value);
        Assert.Equal("NewShip", returned.Name);
    }

    [Fact]
    public void Update_ExistingId_ReturnsOk()
    {
        var request = new ShipRequestDto("UpdatedShip", 55);
        var updated = new ShipResponseDto(1, "UpdatedShip", 55);

        _mockService.Setup(s => s.Update(1, request)).Returns(updated);

        var result = _controller.Update(1, request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<ShipResponseDto>(okResult.Value);
        Assert.Equal("UpdatedShip", returned.Name);
    }

    [Fact]
    public void Update_NonExistingId_ReturnsNotFound()
    {
        var request = new ShipRequestDto("Missing", 60);

        _mockService.Setup(s => s.Update(999, request)).Returns((ShipResponseDto?)null);

        var result = _controller.Update(999, request);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ExistingId_ReturnsOk()
    {
        var deleted = new ShipResponseDto(1, "ToDelete", 40);

        _mockService.Setup(s => s.Delete(1)).Returns(deleted);

        var result = _controller.Delete(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<ShipResponseDto>(okResult.Value);
        Assert.Equal("ToDelete", returned.Name);
    }

    [Fact]
    public void Delete_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.Delete(999)).Returns((ShipResponseDto?)null);

        var result = _controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
