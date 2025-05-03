using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Port> Ports { get; set; }
    public virtual DbSet<Ship> Ships { get; set; }
    public virtual DbSet<Voyage> Voyages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //a country can have one or more ports
        modelBuilder.Entity<Port>()
            .HasOne<Country>(p => p.Country)
            .WithMany(c => c.Ports)
            .HasForeignKey(p => p.IdCountry)
            .OnDelete(DeleteBehavior.Cascade); 
        
        modelBuilder.Entity<Voyage>()
            .HasOne<Ship>(v => v.Ship)
            .WithMany(s => s.Voyages)
            .HasForeignKey(v => v.IdShip)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Voyage>()
            .HasOne(v => v.DeparturePort)
            .WithMany()
            .HasForeignKey(v => v.DeparturePortId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Voyage>()
            .HasOne(v => v.ArrivalPort)
            .WithMany()
            .HasForeignKey(v => v.ArrivalPortId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}