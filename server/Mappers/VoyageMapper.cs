using server.Dtos.Voyage;
using server.Models;

namespace server.Mappers;

public static class VoyageMapper
{
    public static Voyage ToEntity(VoyageRequestDto dto)
    {
        return new Voyage
        {
            VoyageDate = dto.VoyageDate,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            IdShip = dto.IdShip,
            DeparturePortId = dto.DeparturePortId,
            ArrivalPortId = dto.ArrivalPortId
        };
    }

    public static VoyageResponseDto ToDto(Voyage voyage)
    {
        return new VoyageResponseDto(
            voyage.IdVoyage,
            voyage.IdShip,
            voyage.Ship.Name,
            voyage.DeparturePortId,
            voyage.DeparturePort.Name,
            voyage.ArrivalPortId,
            voyage.ArrivalPort.Name,
            voyage.VoyageDate,
            voyage.StartTime,
            voyage.EndTime
        );
    }
}


