using Moq;
using Xunit;
using server.Models;
using server.Dtos.Ship;
using server.Services;
using server.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaritimeApp.Tests.UnitTests.Services;

public class ShipServiceTests
{
    private readonly List<Ship> _ships;
    private readonly ShipService _service;

    public ShipServiceTests()
    {
        _ships = new List<Ship>
        {
            new Ship { IdShip = 1, Name = "Ship1", MaxSpeed = 30 },
            new Ship { IdShip = 2, Name = "Ship2", MaxSpeed = 28 }
        };

        var queryable = _ships.AsQueryable();

        var mockSet = new Mock<DbSet<Ship>>();
        mockSet.As<IQueryable<Ship>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<Ship>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<Ship>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<Ship>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        mockSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _ships.FirstOrDefault(s => s.IdShip == (int)ids[0]));
        mockSet.Setup(d => d.Add(It.IsAny<Ship>())).Callback<Ship>(s => _ships.Add(s));
        mockSet.Setup(d => d.Remove(It.IsAny<Ship>())).Callback<Ship>(s => _ships.Remove(s));

        var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;

        var mockContext = new Mock<ApplicationDbContext>(options);
        mockContext.Setup(c => c.Ships).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChanges()).Returns(1);

        _service = new ShipService(mockContext.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnAllShips()
    {
        var result = _service.GetAll().ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, s => s.Name == "Ship1");
    }

    [Fact]
    public void GetById_ShouldReturnCorrectShip()
    {
        var result = _service.GetById(1);

        Assert.NotNull(result);
        Assert.Equal("Ship1", result!.Name);
    }

    [Fact]
    public void GetById_InvalidId_ShouldReturnNull()
    {
        var result = _service.GetById(99);

        Assert.Null(result);
    }

    [Fact]
    public void Create_ShouldAddNewShip()
    {
        var result = _service.Create(new ShipRequestDto("Created", 35));

        Assert.NotNull(result);
        Assert.Equal("Created", result.Name);
        Assert.Equal(35, result.MaxSpeed);
        Assert.Contains(_ships, s => s.Name == "Created");
    }

    [Fact]
    public void Update_ExistingId_ShouldModifyShip()
    {
        var result = _service.Update(1, new ShipRequestDto("Updated", 40));

        Assert.NotNull(result);
        Assert.Equal("Updated", result!.Name);
        Assert.Equal(40, result.MaxSpeed);
        Assert.Equal("Updated", _ships.First(s => s.IdShip == 1).Name);
    }

    [Fact]
    public void Update_NonExistingId_ShouldReturnNull()
    {
        var result = _service.Update(99, new ShipRequestDto("NonExisting", 50));

        Assert.Null(result);
    }

    [Fact]
    public void Delete_ExistingId_ShouldRemoveShip()
    {
        var result = _service.Delete(1);

        Assert.NotNull(result);
        Assert.DoesNotContain(_ships, s => s.IdShip == 1);
    }

    [Fact]
    public void Delete_NonExistingId_ShouldReturnNull()
    {
        var result = _service.Delete(99);

        Assert.Null(result);
    }
}
