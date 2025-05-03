using server.Dtos.Ship;
using server.Models;

namespace server.Mappers;

public static class ShipMapper
{
    public static Ship ToEntity(ShipRequestDto dto) =>
        new Ship
        {
            Name = dto.Name,
            MaxSpeed = dto.MaxSpeed
        };

    public static ShipResponseDto ToDto(Ship ship) =>
        new ShipResponseDto(
            ship.IdShip,
            ship.Name,
            ship.MaxSpeed
        );
}