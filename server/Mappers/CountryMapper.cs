using server.Dtos.Country;
using server.Models;

namespace server.Mappers;

public static class CountryMapper
{
    public static Country ToEntity(CountryRequestDto countryRequestDto)
    {
        return new Country
        {
            Name = countryRequestDto.Name
        };
    }

    public static CountryResponseDto ToDto(Country country)
    {
        return new CountryResponseDto(country.IdCountry, country.Name);
    }
}