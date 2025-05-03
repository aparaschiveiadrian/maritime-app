using server.Dtos.Country;

namespace server.Services.Interfaces;
public interface ICountryService
{
    IEnumerable<CountryResponseDto> GetAll();
    CountryResponseDto? GetById(int id);
    CountryResponseDto Create(CountryRequestDto dto);
    CountryResponseDto? Update(int id, CountryRequestDto dto);
    CountryResponseDto? Delete(int id);
}