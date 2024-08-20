using AutoMapper;
using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Interfaces.Validations;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;
using DorZnakUA.Application.Validations;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DorZnakUA.Application.Services;

public class ProjectServices : IProjectService
{
    private readonly IBaseRepository<Project> _projectRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IProjectValidator _projectValidator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ProjectServices(IBaseRepository<Project> projectRepository, IBaseRepository<User> userRepository, 
        ILogger logger, IProjectValidator projectValidator, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _logger = logger;
        _projectValidator = projectValidator;
        _mapper = mapper;
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

    /// <inheritdoc/>
    public async Task<BaseResult<ProjectDto>> GetProjectByIdAsync(long id)
    {
        ProjectDto? projectDto;
        try
        {
            projectDto = await _projectRepository
                .GetAll()
                .Select(x => new ProjectDto(x.Id, x.Name, x.Description, x.CreateAt.ToLongDateString()))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.ProjectsNotFound,
            };
        }

        if (projectDto==null)
        {
            _logger.Warning($"Проєкт з {id} не був знайдений",id);
            return new BaseResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.ProjectNotFound,
                ErroreCode = (int)ErrorCodes.ProjectNotFound,
            };
        }

        return new BaseResult<ProjectDto>()
        {
            Date = projectDto,
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ProjectDto>> CreateProjectAsync(CreateProjectDto dto)
    {
        
        try
        {
            var project = await _projectRepository
                .GetAll()
                .FirstOrDefaultAsync(x=>x.Name==dto.Name);
        
            var user= await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x=>x.Id==dto.UserId);

            var result = _projectValidator.CreateValidator(project, user);

            if (!result.IsSeccess)
            {
                return new BaseResult<ProjectDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErroreCode = result.ErroreCode,
                };
            }
            project = new Project()
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = dto.UserId,
            };
            await _projectRepository.CreateAsync(project);
            await _projectRepository.SaveChangesAsync();
            
            return new BaseResult<ProjectDto>()
            {
                Date = _mapper.Map<ProjectDto>(project)
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<ProjectDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.ProjectsNotFound,
            };
        }
    }
}