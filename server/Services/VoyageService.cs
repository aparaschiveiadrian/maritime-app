using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Voyage;
using server.Mappers;
using server.Models;

namespace server.Services;

public class VoyageService
{
    private readonly ApplicationDbContext _context;

    public VoyageService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<VoyageResponseDto> GetAll()
    {
        var voyages = _context.Voyages
            .Include(v => v.Ship)
            .Include(v => v.DeparturePort)
            .Include(v => v.ArrivalPort)
            .ToList();

        return voyages.Select(VoyageMapper.ToDto).ToList();
    }

    public VoyageResponseDto? GetById(int id)
    {
        var voyage = _context.Voyages
            .Include(v => v.Ship)
            .Include(v => v.DeparturePort)
            .Include(v => v.ArrivalPort)
            .FirstOrDefault(v => v.IdVoyage == id);

        return voyage == null ? null : VoyageMapper.ToDto(voyage);
    }

    public VoyageResponseDto Create(VoyageRequestDto dto)
    {
        var voyage = VoyageMapper.ToEntity(dto);
        _context.Voyages.Add(voyage);
        _context.SaveChanges();

        var savedVoyage = _context.Voyages
            .Include(v => v.Ship)
            .Include(v => v.DeparturePort)
            .Include(v => v.ArrivalPort)
            .First(v => v.IdVoyage == voyage.IdVoyage);

        return VoyageMapper.ToDto(savedVoyage);
    }

    public VoyageResponseDto? Update(int id, VoyageRequestDto dto)
    {
        var voyage = _context.Voyages.Find(id);
        if (voyage == null) return null;

        voyage.VoyageDate = dto.VoyageDate;
        voyage.StartTime = dto.StartTime;
        voyage.EndTime = dto.EndTime;
        voyage.IdShip = dto.IdShip;
        voyage.DeparturePortId = dto.DeparturePortId;
        voyage.ArrivalPortId = dto.ArrivalPortId;

        _context.SaveChanges();

        var updatedVoyage = _context.Voyages
            .Include(v => v.Ship)
            .Include(v => v.DeparturePort)
            .Include(v => v.ArrivalPort)
            .First(v => v.IdVoyage == voyage.IdVoyage);

        return VoyageMapper.ToDto(updatedVoyage);
    }

    public VoyageResponseDto? Delete(int id)
    {
        var voyage = _context.Voyages
            .Include(v => v.Ship)
            .Include(v => v.DeparturePort)
            .Include(v => v.ArrivalPort)
            .FirstOrDefault(v => v.IdVoyage == id);

        if (voyage == null) return null;

        _context.Voyages.Remove(voyage);
        _context.SaveChanges();

        return VoyageMapper.ToDto(voyage);
    }
}
