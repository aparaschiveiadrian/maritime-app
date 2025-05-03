using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Ship;

public record ShipRequestDto
(
    [Required]
    [MaxLength(100)]
    string Name,
    
    [Required]
    decimal MaxSpeed
    );