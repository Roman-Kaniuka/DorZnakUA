using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Repositories.DateBases;
using DorZnakUA.DAL.Interceptors;
using DorZnakUA.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DorZnakUA.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer( this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSQL");

        service.AddSingleton<DateInterceptor>();
        service.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        service.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBaseRepository<Project>, BaseRepository<Project>>();
        services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        services.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
        services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
        services.AddScoped<IBaseRepository<UserRole>, BaseRepository<UserRole>>();
        services.AddScoped<IBaseRepository<RoadSign>, BaseRepository<RoadSign>>();
        services.AddScoped<IBaseRepository<MetalRack>, BaseRepository<MetalRack>>();
    }
}