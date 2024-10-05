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

    public override bool Equals(object obj)
    {
        if (obj is MetalRack rack)
        {
            return Math.Round(Height, 3) == Math.Round(rack.Height,3) 
                   && Math.Round(Diameter,3) == Math.Round(rack.Diameter,3) 
                   && Math.Round(Thickness,3) == Math.Round(rack.Thickness,3);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Math.Round(Height, 3), 
            Math.Round(Diameter, 3), 
            Math.Round(Thickness, 3)
            );
    }
}