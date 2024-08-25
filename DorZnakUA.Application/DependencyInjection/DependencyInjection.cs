using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Interfaces.Validations;
using DorZnakUA.Application.Mapping;
using DorZnakUA.Application.Services;
using DorZnakUA.Application.Validations;
using DorZnakUA.Application.Validations.FluentValidations.Project;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DorZnakUA.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProjectMapping));
        
        services.InitServices();
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IProjectValidator, ProjectValidator>();
        services.AddScoped<IValidator<CreateProjectDto>, CreateProjectValodator>();
        services.AddScoped<IValidator<UpdateProjectDto>, UpdateProjectValodator>();
        services.AddScoped<IBaseValidator<User>, UserValidator>();
    }
}