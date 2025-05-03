using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace MaritimeApp.Tests.TestUtils;

public class FakeDbContext : ApplicationDbContext
{
    public FakeDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public override DbSet<Country> Countries { get; set; } = null!;
}
