using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.DorZnakUA.Entity;

public class RoadSign : IEntityId<long>
{
    public long Id { get; set; }
    public string Positioning { get; set; }
    public string PlacementOnRoad { get; set; }
    public int NumberOfRacks { get; set; }

    public Project Project { get; set; }
    public long ProjectId { get; set; }

    public MetalRack MetalRack { get; set; }
    public long? MetalRackId { get; set; }

    public List<Shield> Shields { get; set; }
}