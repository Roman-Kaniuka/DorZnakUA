using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DorZnakUA.Application.Services;

public class ProjectServices : IProjectService
{
    private readonly IBaseRepository<Project> _projectRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly ILogger _logger;

    public ProjectServices(IBaseRepository<Project> projectRepository, IBaseRepository<User> userRepository, 
        ILogger logger)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<CollectionResult<ProjectDto>> GetProjectsAsync(long userId)
    {
        ProjectDto[] projects;

        try
        {
            projects = await _projectRepository
                .GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => new ProjectDto(x.Id, x.Name, x.Description, x.CreateAt.ToLongDateString()))
                .ToArrayAsync();
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new CollectionResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.InternalServerError,
            };
        }

        if (!projects.Any())
        {
            _logger.Warning(ErrorMessage.ProjectsNotFound, projects.Length);
            
            return new CollectionResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.ProjectsNotFound,
            };
        }

        return new CollectionResult<ProjectDto>()
        {
            Date = projects,
            Count = projects.Length
        };
    }
}