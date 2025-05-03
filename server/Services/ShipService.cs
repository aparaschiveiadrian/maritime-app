using server.Data;
using server.Dtos.Ship;
using server.Mappers;
using server.Models;
using server.Services.Interfaces;

namespace server.Services;

public class ShipService : IShipService
{
    private readonly ApplicationDbContext _context;

    public ShipService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ShipResponseDto> GetAll()
    {
        var ships = _context.Ships.ToList();
        return ships.Select(ShipMapper.ToDto).ToList();
    }

    public ShipResponseDto? GetById(int id)
    {
        var ship = _context.Ships.Find(id);
        return ship == null ? null : ShipMapper.ToDto(ship);
    }

    public ShipResponseDto Create(ShipRequestDto dto)
    {
        var ship = ShipMapper.ToEntity(dto);
        _context.Ships.Add(ship);
        _context.SaveChanges();

        return ShipMapper.ToDto(ship);
    }

    public ShipResponseDto? Update(int id, ShipRequestDto dto)
    {
        var ship = _context.Ships.Find(id);
        if (ship == null)
        {
            return null;
        }

        ship.Name = dto.Name;
        ship.MaxSpeed = dto.MaxSpeed;

        _context.SaveChanges();
        return ShipMapper.ToDto(ship);
    }

    public ShipResponseDto? Delete(int id)
    {
        var ship = _context.Ships.Find(id);
        if (ship == null)
        {
            return null;
        }
        _context.Ships.Remove(ship);
        _context.SaveChanges();
        return ShipMapper.ToDto(ship);
    }
}