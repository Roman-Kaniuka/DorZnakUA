namespace Domain.DorZnakUA.Interfaces.Repositories.DateBases;

public interface IStateSaveChanges
{
    Task<int> SaveChangesAsync();
}