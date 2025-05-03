using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using server.Controllers;
using server.Dtos.Port;
using server.Services;
using System.Collections.Generic;
using System.Linq;
using server.Services.Interfaces;

namespace MaritimeApp.Tests.UnitTests.Controllers;

public class PortControllerTests
{
    private readonly Mock<IPortService> _mockService;
    private readonly PortController _controller;

    public PortControllerTests()
    {
        _mockService = new Mock<IPortService>();
        _controller = new PortController(_mockService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOkWithPorts()
    {
        var ports = new List<PortResponseDto>
        {
            new PortResponseDto(1, "Port1", 1),
            new PortResponseDto(2, "Port2", 2)
        };

        _mockService.Setup(s => s.GetAll()).Returns(ports);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsAssignableFrom<IEnumerable<PortResponseDto>>(okResult.Value);
        Assert.Equal(2, returned.Count());
    }

    [Fact]
    public void GetAllWithCountryNames_ReturnsOk()
    {
        var ports = new List<PortWithCountryNameDto>
        {
            new PortWithCountryNameDto(1, "Port1", "Romania"),
            new PortWithCountryNameDto(2, "Port2", "Greece")
        };

        _mockService.Setup(s => s.GetPortsWithCountryNames()).Returns(ports);

        var result = _controller.GetAllWithCountryNames();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsAssignableFrom<IEnumerable<PortWithCountryNameDto>>(okResult.Value);
        Assert.Equal(2, returned.Count());
    }

    [Fact]
    public void GetById_ExistingId_ReturnsOk()
    {
        var port = new PortResponseDto(1, "Port1", 1);

        _mockService.Setup(s => s.GetById(1)).Returns(port);

        var result = _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<PortResponseDto>(okResult.Value);
        Assert.Equal("Port1", returned.Name);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.GetById(999)).Returns((PortResponseDto?)null);

        var result = _controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        var request = new PortRequestDto("NewPort", 1);
        var created = new PortResponseDto(3, "NewPort", 1);

        _mockService.Setup(s => s.Create(request)).Returns(created);

        var result = _controller.Create(request);

        var createdAt = Assert.IsType<CreatedAtActionResult>(result);
        var returned = Assert.IsType<PortResponseDto>(createdAt.Value);
        Assert.Equal("NewPort", returned.Name);
    }

    [Fact]
    public void Update_ExistingId_ReturnsOk()
    {
        var request = new PortRequestDto("UpdatedPort", 1);
        var updated = new PortResponseDto(1, "UpdatedPort", 1);

        _mockService.Setup(s => s.Update(1, request)).Returns(updated);

        var result = _controller.Update(1, request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<PortResponseDto>(okResult.Value);
        Assert.Equal("UpdatedPort", returned.Name);
    }

    [Fact]
    public void Update_NonExistingId_ReturnsNotFound()
    {
        var request = new PortRequestDto("DoesNotExist", 1);

        _mockService.Setup(s => s.Update(999, request)).Returns((PortResponseDto?)null);

        var result = _controller.Update(999, request);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ExistingId_ReturnsOk()
    {
        var deleted = new PortResponseDto(1, "OldPort", 1);

        _mockService.Setup(s => s.Delete(1)).Returns(deleted);

        var result = _controller.Delete(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<PortResponseDto>(okResult.Value);
        Assert.Equal("OldPort", returned.Name);
    }

    [Fact]
    public void Delete_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.Delete(999)).Returns((PortResponseDto?)null);

        var result = _controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
