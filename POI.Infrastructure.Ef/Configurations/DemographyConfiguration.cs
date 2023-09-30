using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POI.Domain.Entities;

namespace POI.Infrastructure.Ef.Configurations;

public class DemographyConfiguration : IEntityTypeConfiguration<Demography>
{
    public void Configure(EntityTypeBuilder<Demography> builder)
    {
        builder.HasKey(x =>x.Id);
        builder.Property(x =>x.Id).ValueGeneratedOnAdd();

        builder.Property(x =>x.Name).HasMaxLength(200);
        builder.Property(x =>x.Address).HasMaxLength(500);
        builder.Property(x =>x.Cat1).HasMaxLength(100);
        builder.Property(x =>x.Cat2).HasMaxLength(100);
        builder.Property(x =>x.Cat3).HasMaxLength(100);
        builder.Property(x =>x.Cat4).HasMaxLength(100);
        builder.Property(x =>x.Cat5).HasMaxLength(100);
        builder.Property(x =>x.CityName).HasMaxLength(100);
        builder.Property(x =>x.CountyName).HasMaxLength(100);
        builder.Property(x =>x.DistrictName).HasMaxLength(100);
    }
}