using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    
    public DbSet<Country> Countries { get; set; }
    public DbSet<Port> Ports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //a country can have one or more ports
        modelBuilder.Entity<Port>()
            .HasOne<Country>(p => p.Country)
            .WithMany(c => c.Ports)
            .HasForeignKey(p => p.IdCountry)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}