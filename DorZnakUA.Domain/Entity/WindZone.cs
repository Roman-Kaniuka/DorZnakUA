using Domain.DorZnakUA.Interfaces;

namespace Domain.DorZnakUA.Entity;

public class WindZone : IEntityId<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Project> Projects { get; set; }
}