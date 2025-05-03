using Moq;
using Xunit;
using server.Models;
using server.Dtos.Port;
using server.Services;
using server.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaritimeApp.Tests.UnitTests.Services;

public class PortServiceTests
{
    private readonly List<Port> _ports;
    private readonly PortService _service;

    public PortServiceTests()
    {
        var mockCountry = new Country { IdCountry = 1, Name = "Italy" };

        _ports = new List<Port>
        {
            new Port { IdPort = 1, Name = "Port A", IdCountry = 1, Country = mockCountry },
            new Port { IdPort = 2, Name = "Port B", IdCountry = 1, Country = mockCountry }
        };

        var queryable = _ports.AsQueryable();

        var mockSet = new Mock<DbSet<Port>>();
        mockSet.As<IQueryable<Port>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<Port>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<Port>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<Port>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        mockSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _ports.FirstOrDefault(p => p.IdPort == (int)ids[0]));
        mockSet.Setup(d => d.Add(It.IsAny<Port>())).Callback<Port>(p => _ports.Add(p));
        mockSet.Setup(d => d.Remove(It.IsAny<Port>())).Callback<Port>(p => _ports.Remove(p));

        var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
        var mockContext = new Mock<ApplicationDbContext>(options);
        mockContext.Setup(c => c.Ports).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChanges()).Returns(1);

        _service = new PortService(mockContext.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnAllPorts()
    {
        var result = _service.GetAll().ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Name == "Port A");
    }

    [Fact]
    public void GetPortsWithCountryNames_ShouldReturnProjectedPorts()
    {
        var result = _service.GetPortsWithCountryNames().ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.Equal("Italy", p.CountryName));
    }

    [Fact]
    public void GetById_ShouldReturnCorrectPort()
    {
        var result = _service.GetById(1);

        Assert.NotNull(result);
        Assert.Equal("Port A", result!.Name);
    }

    [Fact]
    public void GetById_InvalidId_ShouldReturnNull()
    {
        var result = _service.GetById(99);

        Assert.Null(result);
    }

    [Fact]
    public void Create_ShouldAddNewPort()
    {
        var result = _service.Create(new PortRequestDto("New Port", 1));

        Assert.NotNull(result);
        Assert.Equal("New Port", result.Name);
        Assert.Contains(_ports, p => p.Name == "New Port");
    }

    [Fact]
    public void Update_ExistingId_ShouldModifyPort()
    {
        var result = _service.Update(1, new PortRequestDto("Updated Port", 1));

        Assert.NotNull(result);
        Assert.Equal("Updated Port", result!.Name);
        Assert.Equal("Updated Port", _ports.First(p => p.IdPort == 1).Name);
    }

    [Fact]
    public void Update_NonExistingId_ShouldReturnNull()
    {
        var result = _service.Update(99, new PortRequestDto("NonExisting", 2));

        Assert.Null(result);
    }

    [Fact]
    public void Delete_ExistingId_ShouldRemovePort()
    {
        var result = _service.Delete(1);

        Assert.NotNull(result);
        Assert.DoesNotContain(_ports, p => p.IdPort == 1);
    }

    [Fact]
    public void Delete_NonExistingId_ShouldReturnNull()
    {
        var result = _service.Delete(99);

        Assert.Null(result);
    }
}
