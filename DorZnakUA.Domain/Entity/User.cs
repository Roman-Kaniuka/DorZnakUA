using Domain.DorZnakUA.Interfaces;

namespace Domain.DorZnakUA.Entity;

public class User : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    public string Login  { get; set; }
    public string Password { get; set; }

    public List<Project> Projects { get; set; }

    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public long CreateBy { get; set; }
}