using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.EntityFrameworkCore;

namespace DorZnakUA.Application.Services;

public class ProjectServices : IProjectService
{
    private readonly IBaseRepository<Project> _projectRepository;
    private readonly IBaseRepository<User> _userRepository;

    public ProjectServices(IBaseRepository<Project> projectRepository, IBaseRepository<User> userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

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
            Console.WriteLine(e);
            throw;
        }

    }
}