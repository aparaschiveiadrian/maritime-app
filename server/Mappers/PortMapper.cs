using server.Dtos.Port;
using server.Models;

namespace server.Mappers;

public static class PortMapper
{
    public static Port toEntity(PortRequestDto portRequestDto)
    {
        return new Port
        {
            Name = portRequestDto.Name,
            IdCountry = portRequestDto.IdCountry
        };
    }

    public static PortResponseDto toDto(Port port)
    {
        return new PortResponseDto(port.IdPort, port.Name, port.IdCountry);
    }
}