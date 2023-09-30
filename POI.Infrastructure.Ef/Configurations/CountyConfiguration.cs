using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POI.Domain.Entities;

namespace POI.Infrastructure.Ef.Configurations;

public class CountyConfiguration : IEntityTypeConfiguration<County>
{
    public void Configure(EntityTypeBuilder<County> builder)
    {
        builder.ToTable("County");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Geography)
            .HasColumnType("geography");

        builder.HasOne(e => e.City)
            .WithMany(c => c.Counties)
            .HasForeignKey(e => e.CityId);
    }
}