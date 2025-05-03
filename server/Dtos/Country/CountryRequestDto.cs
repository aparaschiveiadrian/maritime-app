using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Country;

public record CountryRequestDto(
    [Required]
    [MaxLength(100)]
    string Name
);