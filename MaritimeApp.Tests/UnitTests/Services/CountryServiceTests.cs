using Moq;
using Xunit;
using server.Models;
using server.Dtos.Country;
using server.Services;
using server.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MaritimeApp.Tests.TestUtils;

namespace MaritimeApp.Tests.UnitTests.Services;

public class CountryServiceTests
{
    private readonly List<Country> _countries;
    private readonly CountryService _service;

    public CountryServiceTests()
    {
        _countries = new List<Country>
        {
            new Country { IdCountry = 1, Name = "Romania" },
            new Country { IdCountry = 2, Name = "Greece" }
        };

        var queryable = _countries.AsQueryable();

        var mockSet = new Mock<DbSet<Country>>();
        mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        mockSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => _countries.FirstOrDefault(c => c.IdCountry == (int)ids[0]));
        mockSet.Setup(d => d.Add(It.IsAny<Country>())).Callback<Country>(c => _countries.Add(c));
        mockSet.Setup(d => d.Remove(It.IsAny<Country>())).Callback<Country>(c => _countries.Remove(c));

        var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
        var fakeContext = new FakeDbContext(options)
        {
            Countries = mockSet.Object
        };

        var mockContext = new Mock<ApplicationDbContext>(options);
        mockContext.Setup(c => c.Countries).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChanges()).Returns(1);

        _service = new CountryService(mockContext.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnAllCountries()
    {
        var result = _service.GetAll().ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Name == "Romania");
    }

    [Fact]
    public void GetById_ShouldReturnCorrectCountry()
    {
        var result = _service.GetById(1);

        Assert.NotNull(result);
        Assert.Equal("Romania", result!.Name);
    }

    [Fact]
    public void GetById_InvalidId_ShouldReturnNull()
    {
        var result = _service.GetById(99);

        Assert.Null(result);
    }

    [Fact]
    public void Create_ShouldAddNewCountry()
    {
        var result = _service.Create(new CountryRequestDto("Germany"));

        Assert.NotNull(result);
        Assert.Equal("Germany", result.Name);
        Assert.Contains(_countries, c => c.Name == "Germany");
    }

    [Fact]
    public void Update_ExistingId_ShouldModifyCountry()
    {
        var result = _service.Update(1, new CountryRequestDto("UpdatedName"));

        Assert.NotNull(result);
        Assert.Equal("UpdatedName", result!.Name);
        Assert.Equal("UpdatedName", _countries.First(c => c.IdCountry == 1).Name);
    }

    [Fact]
    public void Update_NonExistingId_ShouldReturnNull()
    {
        var result = _service.Update(99, new CountryRequestDto("Update"));

        Assert.Null(result);
    }

    [Fact]
    public void Delete_ExistingId_ShouldRemoveCountry()
    {
        var result = _service.Delete(1);

        Assert.NotNull(result);
        Assert.DoesNotContain(_countries, c => c.IdCountry == 1);
    }

    [Fact]
    public void Delete_NonExistingId_ShouldReturnNull()
    {
        var result = _service.Delete(99);

        Assert.Null(result);
    }
}
