using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using server.Controllers;
using server.Dtos.Voyage;
using server.Services;
using System.Collections.Generic;
using System.Linq;
using server.Services.Interfaces;

namespace MaritimeApp.Tests.UnitTests.Controllers;

public class VoyageControllerTests
{
    private readonly Mock<IVoyageService> _mockService;
    private readonly VoyageController _controller;

    public VoyageControllerTests()
    {
        _mockService = new Mock<IVoyageService>();
        _controller = new VoyageController(_mockService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOkWithVoyages()
    {
        var voyages = new List<VoyageResponseDto>
        {
            new VoyageResponseDto(1, 1, "Ship1", 1, "PortA", 2, "PortB", DateTime.Now, DateTime.Now, DateTime.Now),
            new VoyageResponseDto(2, 2, "Ship2", 3, "PortC", 4, "PortD", DateTime.Now, DateTime.Now, DateTime.Now)
        };

        _mockService.Setup(s => s.GetAll()).Returns(voyages);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsAssignableFrom<IEnumerable<VoyageResponseDto>>(okResult.Value);
        Assert.Equal(2, returned.Count());
    }

    [Fact]
    public void GetById_ExistingId_ReturnsOk()
    {
        var voyage = new VoyageResponseDto(1, 1, "Ship1", 1, "PortA", 2, "PortB", DateTime.Now, DateTime.Now, DateTime.Now);

        _mockService.Setup(s => s.GetById(1)).Returns(voyage);

        var result = _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<VoyageResponseDto>(okResult.Value);
        Assert.Equal(1, returned.IdVoyage);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.GetById(999)).Returns((VoyageResponseDto?)null);

        var result = _controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        var request = new VoyageRequestDto(DateTime.Now, DateTime.Now, DateTime.Now, 1, 1, 2);
        var created = new VoyageResponseDto(3, 1, "Ship3", 1, "PortA", 2, "PortB", DateTime.Now, DateTime.Now, DateTime.Now);

        _mockService.Setup(s => s.Create(request)).Returns(created);

        var result = _controller.Create(request);

        var createdAt = Assert.IsType<CreatedAtActionResult>(result);
        var returned = Assert.IsType<VoyageResponseDto>(createdAt.Value);
        Assert.Equal(3, returned.IdVoyage);
    }

    [Fact]
    public void Update_ExistingId_ReturnsOk()
    {
        var request = new VoyageRequestDto( DateTime.Now, DateTime.Now, DateTime.Now, 1, 1, 2);
        var updated = new VoyageResponseDto(1, 1, "UpdatedShip", 1, "PortA", 2, "PortB", DateTime.Now, DateTime.Now, DateTime.Now);

        _mockService.Setup(s => s.Update(1, request)).Returns(updated);

        var result = _controller.Update(1, request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<VoyageResponseDto>(okResult.Value);
        Assert.Equal("UpdatedShip", returned.ShipName);
    }

    [Fact]
    public void Update_NonExistingId_ReturnsNotFound()
    {
        var request = new VoyageRequestDto( DateTime.Now, DateTime.Now, DateTime.Now, 1, 1, 2);

        _mockService.Setup(s => s.Update(999, request)).Returns((VoyageResponseDto?)null);

        var result = _controller.Update(999, request);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ExistingId_ReturnsOk()
    {
        var deleted = new VoyageResponseDto(1, 1, "ToDelete", 1, "PortA", 2, "PortB", DateTime.Now, DateTime.Now, DateTime.Now);

        _mockService.Setup(s => s.Delete(1)).Returns(deleted);

        var result = _controller.Delete(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<VoyageResponseDto>(okResult.Value);
        Assert.Equal("ToDelete", returned.ShipName);
    }

    [Fact]
    public void Delete_NonExistingId_ReturnsNotFound()
    {
        _mockService.Setup(s => s.Delete(999)).Returns((VoyageResponseDto?)null);

        var result = _controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
