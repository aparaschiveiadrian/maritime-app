namespace server.Dtos.Port;

public record PortResponseDto(
    int IdPort,
    string Name,
    int IdCountry
);