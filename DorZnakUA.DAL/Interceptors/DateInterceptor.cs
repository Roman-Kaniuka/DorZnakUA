using Domain.DorZnakUA.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DorZnakUA.DAL.Interceptors;

public class DateInterceptor : SaveChangesInterceptor
{
     public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
          CancellationToken cancellationToken = new CancellationToken())
     {
          var dbContext = eventData.Context;
          if (dbContext == null)
          {
               return base.SavingChangesAsync(eventData, result, cancellationToken);
          }
          
          var entries = dbContext.ChangeTracker.Entries<IAuditable>()
               .Where(x=>x.State == EntityState.Added || x.State == EntityState.Modified)
               .ToList();
          foreach (var entry in entries)
          {
               if (entry.State == EntityState.Added)
               {
                    entry.Property(x=>x.CreateAt).CurrentValue = DateTime.UtcNow;
               }

               if (entry.State == EntityState.Modified)
               {
                    entry.Property(x => x.UpdateAt).CurrentValue = DateTime.UtcNow;
               }
          }
          return base.SavingChangesAsync(eventData, result, cancellationToken);
     }
}