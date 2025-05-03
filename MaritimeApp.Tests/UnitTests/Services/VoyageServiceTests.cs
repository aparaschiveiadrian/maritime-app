using Moq;
using Xunit;
using server.Models;
using server.Dtos.Voyage;
using server.Services;
using server.Data;
using server.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaritimeApp.Tests.UnitTests.Services;

public class VoyageServiceTests
{
    private readonly List<Voyage> _voyages;
    private readonly VoyageService _service;
    private readonly Ship _ship;
    private readonly Port _departurePort;
    private readonly Port _arrivalPort;

    public VoyageServiceTests()
    {
        _ship = new Ship { IdShip = 1, Name = "Ship1", MaxSpeed = 25 };
        _departurePort = new Port { IdPort = 1, Name = "Port A" };
        _arrivalPort = new Port { IdPort = 2, Name = "Port B" };

        _voyages = new List<Voyage>
        {
            new Voyage
            {
                IdVoyage = 1,
                VoyageDate = DateTime.Now.Date,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(5),
                IdShip = 1,
                DeparturePortId = 1,
                ArrivalPortId = 2,
                Ship = _ship,
                DeparturePort = _departurePort,
                ArrivalPort = _arrivalPort
            }
        };

        var queryable = _voyages.AsQueryable();

        var mockSet = new Mock<DbSet<Voyage>>();
        mockSet.As<IQueryable<Voyage>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<Voyage>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<Voyage>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<Voyage>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        mockSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _voyages.FirstOrDefault(v => v.IdVoyage == (int)ids[0]));
        mockSet.Setup(d => d.Add(It.IsAny<Voyage>())).Callback<Voyage>(v => {
            v.IdVoyage = _voyages.Max(vg => vg.IdVoyage) + 1;
            v.Ship = _ship;
            v.DeparturePort = _departurePort;
            v.ArrivalPort = _arrivalPort;
            _voyages.Add(v);
        });
        mockSet.Setup(d => d.Remove(It.IsAny<Voyage>())).Callback<Voyage>(v => _voyages.Remove(v));

        var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
        var mockContext = new Mock<ApplicationDbContext>(options);
        mockContext.Setup(c => c.Voyages).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChanges()).Returns(1);

        _service = new VoyageService(mockContext.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnAllVoyages()
    {
        var result = _service.GetAll().ToList();

        Assert.Single(result);
        Assert.Equal("Ship1", result[0].ShipName);
    }

    [Fact]
    public void GetById_ShouldReturnVoyage()
    {
        var result = _service.GetById(1);

        Assert.NotNull(result);
        Assert.Equal(1, result!.IdVoyage);
    }

    [Fact]
    public void GetById_InvalidId_ShouldReturnNull()
    {
        var result = _service.GetById(99);

        Assert.Null(result);
    }

    [Fact]
    public void Create_ShouldAddNewVoyage()
    {
        var newDto = new VoyageRequestDto(
            VoyageDate: DateTime.Today.AddDays(1),
            StartTime: DateTime.Today.AddHours(10),
            EndTime: DateTime.Today.AddHours(16),
            IdShip: 1,
            DeparturePortId: 1,
            ArrivalPortId: 2
        );

        var result = _service.Create(newDto);

        Assert.NotNull(result);
        Assert.Equal("Ship1", result.ShipName);
        Assert.Equal(2, _voyages.Count);
    }

    [Fact]
    public void Update_ExistingId_ShouldModifyVoyage()
    {
        var updateDto = new VoyageRequestDto(
            VoyageDate: DateTime.Today,
            StartTime: DateTime.Today.AddHours(1),
            EndTime: DateTime.Today.AddHours(6),
            IdShip: 1,
            DeparturePortId: 1,
            ArrivalPortId: 2
        );

        var result = _service.Update(1, updateDto);

        Assert.NotNull(result);
        Assert.Equal(DateTime.Today.AddHours(1), _voyages[0].StartTime);
    }

    [Fact]
    public void Update_NonExistingId_ShouldReturnNull()
    {
        var updateDto = new VoyageRequestDto(
            VoyageDate: DateTime.Today,
            StartTime: DateTime.Today.AddHours(1),
            EndTime: DateTime.Today.AddHours(6),
            IdShip: 1,
            DeparturePortId: 1,
            ArrivalPortId: 2
        );

        var result = _service.Update(99, updateDto);

        Assert.Null(result);
    }

    [Fact]
    public void Delete_ExistingId_ShouldRemoveVoyage()
    {
        var result = _service.Delete(1);

        Assert.NotNull(result);
        Assert.Empty(_voyages);
    }

    [Fact]
    public void Delete_NonExistingId_ShouldReturnNull()
    {
        var result = _service.Delete(99);

        Assert.Null(result);
    }
}
