using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POI.Domain.Entities;

namespace POI.Infrastructure.Ef.Configurations;


public class PoiDistanceConfiguration : IEntityTypeConfiguration<PoiDistance>
{
    public void Configure(EntityTypeBuilder<PoiDistance> builder)
    {
        builder.ToTable("PoiDistances");

        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();

        builder.Property(x=>x.Distance).IsRequired();

        builder.HasOne(x => x.FirstPoiCatalog)
            .WithMany(p => p.FirstPoiDistances)
            .HasForeignKey(x => x.FirstPoiCatalogId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x=>x.SecondPoiCatalog)
            .WithMany()
            .HasForeignKey(x=>x.SecondPoiCatalogId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}