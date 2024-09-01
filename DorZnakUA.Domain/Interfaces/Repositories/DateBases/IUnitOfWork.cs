using Domain.DorZnakUA.Entity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.DorZnakUA.Interfaces.Repositories.DateBases;

public interface IUnitOfWork : IStateSaveChanges
{
    IBaseRepository<User> Users { get; set; }
    IBaseRepository<Role> Roles { get; set; }
    IBaseRepository<UserRole> UserRoles { get; set; }
    Task<IDbContextTransaction> BeginTransactionAsync();
}