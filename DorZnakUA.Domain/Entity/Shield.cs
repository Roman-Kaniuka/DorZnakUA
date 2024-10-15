using Domain.DorZnakUA.Interfaces;

namespace Domain.DorZnakUA.Entity;

public class Shield : IEntityId<long>  
{
    public long Id { get; set; }
    public string Group { get; set; }
    public string Name { get; set; }
    public string Shape { get; set; }
    public string SizeType { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Weight { get; set; }

    public List<RoadSign> RoadSigns { get; set; }
    
    
    
}