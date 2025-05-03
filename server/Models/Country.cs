using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Country
{
    [Key]
    public int IdCountry { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}