using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Port;
using server.Mappers;

namespace server.Services;

public class PortService
{
    private readonly ApplicationDbContext _context;

    public PortService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<PortResponseDto> GetAll()
    {
        var ports = _context.Ports.Include(p => p.Country).ToList();
        return ports.Select(PortMapper.toDto).ToList();
    }
    
    public IEnumerable<PortWithCountryNameDto> GetPortsWithCountryNames()
    {
        return _context.Ports
            .Include(p => p.Country)
            .Select(p => new PortWithCountryNameDto(
                p.IdPort,
                p.Name,
                p.Country.Name
            ))
            .ToList();
    }

    public PortResponseDto? GetById(int id)
    {
        var port = _context.Ports.Include(p => p.Country).FirstOrDefault(p => p.IdPort == id);
        return port == null ? null : PortMapper.toDto(port);
    }

    public PortResponseDto Create(PortRequestDto dto)
    {
        var portToCreate = PortMapper.toEntity(dto);
        _context.Ports.Add(portToCreate);
        _context.SaveChanges();
        
        return PortMapper.toDto(portToCreate);
    }

    public PortResponseDto? Update(int id, PortRequestDto dto)
    {
        var existingPort = _context.Ports.Find(id);

        if (existingPort == null)
        {
            return null;
        }
        
        existingPort.Name = dto.Name;
        existingPort.IdCountry = dto.IdCountry;
        
        _context.Ports.Update(existingPort);
        _context.SaveChanges();

        return PortMapper.toDto(existingPort);
    }
    
    public PortResponseDto? Delete(int id)
    {
        var port = _context.Ports.Find(id);
        if (port == null)
        {
            return null;
        }

        _context.Ports.Remove(port);
        _context.SaveChanges();

        return PortMapper.toDto(port);
    }
}