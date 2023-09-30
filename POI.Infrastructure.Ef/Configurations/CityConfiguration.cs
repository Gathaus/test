using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POI.Domain.Entities;

namespace POI.Infrastructure.Ef.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("City");

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Geography)
            .HasColumnType("geography");
        
        builder.HasOne(e => e.Country)
            .WithMany(c => c.Cities)
            .HasForeignKey(e => e.CountryId);

        builder.HasMany(e => e.Counties)
            .WithOne(c => c.City)
            .HasForeignKey(c => c.CityId);
    }
}