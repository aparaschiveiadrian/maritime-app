using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Voyage;

public record VoyageRequestDto(
    [Required] DateTime VoyageDate,
    [Required] DateTime StartTime,
    [Required] DateTime EndTime,
    [Required] int IdShip,
    [Required] int DeparturePortId,
    [Required] int ArrivalPortId
);