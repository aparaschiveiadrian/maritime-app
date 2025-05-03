using server.Dtos.Ship;

namespace server.Services.Interfaces;

public interface IShipService
{
    IEnumerable<ShipResponseDto> GetAll();
    ShipResponseDto? GetById(int id);
    ShipResponseDto Create(ShipRequestDto dto);
    ShipResponseDto? Update(int id, ShipRequestDto dto);
    ShipResponseDto? Delete(int id);
}