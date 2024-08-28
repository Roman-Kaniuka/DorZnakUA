using Domain.DorZnakUA.Interfaces;

namespace Domain.DorZnakUA.Entity;

public class Role : IEntityId<int>
{
    public int Id { get; set; }
    public string  Name { get; set; }
    public List<User> Users { get; set; }
}