namespace Domain.DorZnakUA.Interfaces;

public interface IAuditable
{
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public long CreateBy { get; set; }
}