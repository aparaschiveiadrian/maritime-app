using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Ship
{
    [Key]
    public int IdShip { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal MaxSpeed { get; set; }
    
    public ICollection<Voyage> Voyages { get; set; } = null!;
}