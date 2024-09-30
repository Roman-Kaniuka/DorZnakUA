using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class RoadSingConfiguration : IEntityTypeConfiguration<RoadSign>
{
    public void Configure(EntityTypeBuilder<RoadSign> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Positioning).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PlacementOnRoad).IsRequired().HasMaxLength(50);
        builder.Property(x => x.NumberOfRacks).IsRequired();

        builder.HasData(new List<RoadSign>()
        {
            new RoadSign()
            {
                Id = 1,
                Positioning = "110+50",
                PlacementOnRoad = (nameof(PlacementOnRoad.Right)),
                NumberOfRacks = 1,
                ProjectId = 1,
                MetalRackId = 1,
            },
            new RoadSign()
            {
                Id = 2,
                Positioning = "112+15",
                PlacementOnRoad = (nameof(PlacementOnRoad.InTheMiddle)),
                NumberOfRacks = 2,
                ProjectId = 1,
                MetalRackId = null,
            },
            new RoadSign()
            {
                Id = 3,
                Positioning = "115+98",
                PlacementOnRoad = (nameof(PlacementOnRoad.Left)),
                NumberOfRacks = 1,
                ProjectId = 2,
                MetalRackId = 4
            },
        });
    }
}