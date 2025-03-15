using Domain.DorZnakUA.Dto.MetalRack;
using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Dto.RoadSign;
using Domain.DorZnakUA.Dto.Role;
using Domain.DorZnakUA.Dto.Shield;
using Domain.DorZnakUA.Dto.WindZone;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Interfaces.Validations;
using DorZnakUA.Application.Mapping;
using DorZnakUA.Application.Services;
using DorZnakUA.Application.Validations;
using DorZnakUA.Application.Validations.FluentValidations.MetalRack;
using DorZnakUA.Application.Validations.FluentValidations.Project;
using DorZnakUA.Application.Validations.FluentValidations.RoadSign;
using DorZnakUA.Application.Validations.FluentValidations.Role;
using DorZnakUA.Application.Validations.FluentValidations.Shield;
using DorZnakUA.Application.Validations.FluentValidations.WindZone;
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
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRoadSignService, RoadSignService>();
        services.AddScoped<IMetalRackService, MetalRackService>();
        services.AddScoped<IWindZoneService, WindZoneService>();
        services.AddScoped<IShieldService, ShieldService>();

        services.AddScoped<IProjectValidator, ProjectValidator>();
        services.AddScoped<IValidator<CreateProjectDto>, CreateProjectValidator>();
        services.AddScoped<IValidator<UpdateProjectDto>, UpdateProjectValidator>();
        services.AddScoped<IValidator<CreateMetalRackDto>,CreateMetalRackValidator>();
        services.AddScoped<IValidator<UpdateMetalRackDto>, UpdateMetalRackValidator>();
        services.AddScoped<IValidator<CreateRoadSignDto>, CreateRoadSignValidator>();
        services.AddScoped<IValidator<UpdateRoadSignDto>, UpdateRoadSignValidator>();
        services.AddScoped<IValidator<CreateRoleDto>, CreateRoleValidator>();
        services.AddScoped<IValidator<RoleDto>, UpdateRoleValidator>();
        services.AddScoped<IValidator<CreateShieldDto>, CreateShieldValidator>();
        services.AddScoped<IValidator<UpdateShieldDto>, UpdateShieldValidator>();
        services.AddScoped<IValidator<CreateWindZoneDto>, CreateWindZoneValidator>();
        services.AddScoped<IValidator<UpdateWindZoneDto>, UpdateWindZoneValidator>();
        services.AddScoped<IBaseValidator<User>, UserValidator>();
        services.AddScoped<IBaseValidator<Project>, ProjectValidator>();
    }
}