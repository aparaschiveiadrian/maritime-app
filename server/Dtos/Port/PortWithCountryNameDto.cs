namespace server.Dtos.Port;

public record PortWithCountryNameDto(
    int IdPort,
    string Name,
    string CountryName
);