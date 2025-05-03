using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Port
{
    [Key]
    public int IdPort { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    //FK
    public int IdCountry { get; set; }
    public Country Country { get; set; } = null!;
}