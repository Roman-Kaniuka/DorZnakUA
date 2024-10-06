using Domain.DorZnakUA.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class MetalRackConfiguration : IEntityTypeConfiguration<MetalRack>
{
    public void Configure(EntityTypeBuilder<MetalRack> builder)
    {
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Height).IsRequired();
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.Diameter).IsRequired();
        builder.Property(x => x.Thickness).IsRequired();

        builder.HasMany<RoadSign>(x => x.RoadSigns)
            .WithOne(x => x.MetalRack)
            .HasForeignKey(x => x.MetalRackId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.SetNull);;

        builder.HasData(new List<MetalRack>()
        {
            new MetalRack()
            {
                Id = 1,
                Name = "СКМ1.20",
                Height = 2.0,
                Weight = 5.5,
                Diameter = 0.04,
                Thickness = 0.003
            },
            
            new MetalRack()
            {
                Id = 2,
                Name = "СКМ1.25",
                Height = 2.5,
                Weight = 6.9,
                Diameter = 0.04,
                Thickness = 0.003
            },
            
            new MetalRack()
            {
                Id = 3,
                Name = "СКМ1.30",
                Height = 3.0,
                Weight = 8.2,
                Diameter = 0.04,
                Thickness = 0.003
            },
            
            
            new MetalRack()
            {
                Id = 4,
                Name = "СКМ1.35",
                Height = 3.5,
                Weight = 9.6,
                Diameter = 0.04,
                Thickness = 0.003
            },

        });
    }
}