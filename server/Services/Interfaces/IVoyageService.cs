namespace server.Services.Interfaces;

using server.Dtos.Voyage;

public interface IVoyageService
{
    IEnumerable<VoyageResponseDto> GetAll();
    VoyageResponseDto? GetById(int id);
    VoyageResponseDto Create(VoyageRequestDto dto);
    VoyageResponseDto? Update(int id, VoyageRequestDto dto);
    VoyageResponseDto? Delete(int id);
}
