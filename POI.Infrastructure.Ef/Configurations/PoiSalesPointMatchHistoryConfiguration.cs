using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POI.Domain.Entities;

namespace POI.Infrastructure.Ef.Configurations;


public class PoiSalesPointMatchHistoryConfiguration : IEntityTypeConfiguration<PoiSalesPointMatchHistory>
{
    public void Configure(EntityTypeBuilder<PoiSalesPointMatchHistory> builder)
    {
        builder.ToTable("PoiSalesPointMatchHistories");

        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();

        builder.Property(x=>x.PoiId).IsRequired();
        builder.Property(x=>x.SalesPointId).IsRequired();
        builder.Property(x=>x.MatchDate).IsRequired();
        builder.Property(x=>x.MatchSource).HasMaxLength(250);

        builder.HasOne(x=>x.PoiCatalog)
            .WithMany()
            .HasForeignKey(x=>x.PoiId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}