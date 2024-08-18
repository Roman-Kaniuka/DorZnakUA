using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DorZnakUA.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer( this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSQL");

        service.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
    
}