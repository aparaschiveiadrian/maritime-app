using server.Dtos.Port;

namespace server.Services.Interfaces;

public interface IPortService
{
    IEnumerable<PortResponseDto> GetAll();
    IEnumerable<PortWithCountryNameDto> GetPortsWithCountryNames();
    PortResponseDto? GetById(int id);
    PortResponseDto Create(PortRequestDto dto);
    PortResponseDto? Update(int id, PortRequestDto dto);
    PortResponseDto? Delete(int id);
}