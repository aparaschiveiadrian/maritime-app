namespace server.Dtos.Ship;

public record ShipResponseDto(
    int IdShip,
    string Name,
    decimal MaxSpeed
);
