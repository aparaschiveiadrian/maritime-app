using server.Data;
using server.Dtos.Country;
using server.Mappers;
using server.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using server.Services.Interfaces;

namespace server.Services;

public class CountryService : ICountryService
{
    private readonly ApplicationDbContext _context;

    public CountryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<CountryResponseDto> GetAll()
    {
        var countries = _context.Countries.ToList();
        return countries.Select(CountryMapper.ToDto).ToList();
    }

    public CountryResponseDto? GetById(int id)
    {
        var country = _context.Countries.Find(id);
        return country == null ? null : CountryMapper.ToDto(country);
    }

    public CountryResponseDto Create(CountryRequestDto dto)
    {
        var countryEntity = CountryMapper.ToEntity(dto);
        _context.Countries.Add(countryEntity);
        _context.SaveChanges();

        return CountryMapper.ToDto(countryEntity);
    }

    public CountryResponseDto? Update(int id, CountryRequestDto dto)
    {
        var countryToUpdate = _context.Countries.Find(id);
        if (countryToUpdate == null)
        {
            return null;
        }

        countryToUpdate.Name = dto.Name;
        _context.Countries.Update(countryToUpdate);
        _context.SaveChanges();

        return CountryMapper.ToDto(countryToUpdate);
    }

    public CountryResponseDto? Delete(int id)
    {
        var countryToDelete = _context.Countries.Find(id);
        if (countryToDelete == null)
        {
            return null;
        }

        _context.Countries.Remove(countryToDelete);
        _context.SaveChanges();

        return CountryMapper.ToDto(countryToDelete);
    }
}