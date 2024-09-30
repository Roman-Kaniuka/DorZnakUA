using Domain.DorZnakUA.Interfaces;

namespace Domain.DorZnakUA.Entity;

public class MetalRack : IEntityId<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public double Diameter { get; set; }

    public double Thickness { get; set; }

    public List<RoadSign> RoadSigns { get; set; }
}