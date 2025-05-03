using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Port;

public record PortRequestDto(
    [Required]
    [MaxLength(100)]
    string Name,
    
    [Required]
    int IdCountry
    );