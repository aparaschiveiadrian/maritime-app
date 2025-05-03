namespace server.Dtos.Voyage;

public record VoyageResponseDto(
    int IdVoyage,
    int IdShip,
    string ShipName,
    int DeparturePortId,
    string DeparturePortName,
    int ArrivalPortId,
    string ArrivalPortName,
    DateTime VoyageDate,
    DateTime StartTime,
    DateTime EndTime
);
