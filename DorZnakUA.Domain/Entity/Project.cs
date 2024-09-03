using Domain.DorZnakUA.Interfaces;

namespace Domain.DorZnakUA.Entity;

public class Project : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public User User { get; set; }
    public long UserId { get; set; }

    public List<RoadSign> RoadSigns { get; set; }
    
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public long? CreateBy { get; set; }
}