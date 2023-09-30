using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POI.Domain.Entities;

namespace POI.Infrastructure.Ef.Configurations;

public class PoiCatalogConfiguration : IEntityTypeConfiguration<PoiCatalog>
{
    public void Configure(EntityTypeBuilder<PoiCatalog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
            
        builder.Property(x => x.Name).HasMaxLength(250);
        builder.Property(x => x.NameOnBoard).HasMaxLength(250);
        builder.Property(x => x.CountryName).HasMaxLength(100);
        builder.Property(x => x.CityName).HasMaxLength(100);
        builder.Property(x => x.CountyName).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(500);
        builder.Property(x => x.Geography).HasColumnType("geography");

        
        builder.Property(x => x.CountryId).IsRequired();
        builder.Property(x => x.CityId).IsRequired();
        builder.Property(x => x.CountyId).IsRequired();
        
        builder.Property(x => x.IsPassive).HasDefaultValue(false);
        
        // Relationships
        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.County)
            .WithMany()
            .HasForeignKey(x => x.CountyId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}