using POI.Domain.Entities.ExaEntities.Models;
using Microsoft.EntityFrameworkCore;

namespace POI.Infrastructure.Ef.Context;

public class AgentDbContext : DbContext
{
    public AgentDbContext(DbContextOptions<AgentDbContext> options) : base(options)
    {
        Database.SetCommandTimeout(10000);
    }

    public AgentDbContext() : base(new DbContextOptionsBuilder<AgentDbContext>()
    {
    }

    public DbSet<SalesPoint> SalesPoint { get; set; }
    public DbSet<Company> Company { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(
            x => x.UseNetTopologySuite());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<SalesPoint>()
            .ToTable(tb => tb.HasTrigger("[trg_SalesPoint_SearchTextGenerate_FromSalesPoint]"));
  }
}