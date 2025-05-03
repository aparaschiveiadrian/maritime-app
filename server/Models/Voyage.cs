using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class Voyage
{
    [Key]
    public int IdVoyage { get; set; }

    [Required]
    public DateTime VoyageDate { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    //FK
    public int IdShip { get; set; }
    public Ship Ship { get; set; } = null!;

    public int DeparturePortId { get; set; }
    public Port DeparturePort { get; set; } = null!;

    public int ArrivalPortId { get; set; }
    public Port ArrivalPort { get; set; } = null!;
}