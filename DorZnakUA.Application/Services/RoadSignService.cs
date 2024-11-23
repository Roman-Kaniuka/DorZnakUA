using AutoMapper;
using Domain.DorZnakUA.Dto.RoadSign;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Interfaces.Validations;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DorZnakUA.Application.Services;

public class RoadSignService : IRoadSignService
{
    private readonly IBaseRepository<Project> _projectRepository;
    private readonly IBaseRepository<RoadSign> _roadSignRepository;
    private readonly IBaseValidator<Project> _projectValidator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public RoadSignService(IBaseRepository<Project> projectRepository, IBaseRepository<RoadSign> roadSignRepository,
        IMapper mapper, ILogger logger, IBaseValidator<Project> projectValidator)
    {
        _projectRepository = projectRepository;
        _roadSignRepository = roadSignRepository;
        _mapper = mapper;
        _logger = logger;
        _projectValidator = projectValidator;
    }

    public async Task<CollectionResult<RoadSignDto>> GetRoadSignsAsync(long projectId)
    {
        RoadSignDto[] roadSigns;
        try
        {
            roadSigns = await _roadSignRepository
                .GetAll()
                .AsNoTracking()
                .Where(x => x.ProjectId == projectId)
                .Select(x => new RoadSignDto(x.Id, x.Positioning, x.PlacementOnRoad, x.NumberOfRacks))
                .ToArrayAsync();
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new CollectionResult<RoadSignDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }

        if (!roadSigns.Any())
        {
            _logger.Warning($"Проєкт з вказаним id: {projectId} не містить жодного знаку");
            return new CollectionResult<RoadSignDto>()
            {
                ErrorMessage = ErrorMessage.RoadSignsNotFound,
                ErroreCode = (int) ErrorCodes.RoadSignsNotFound,
            };
        }

        return new CollectionResult<RoadSignDto>()
        {
            Date = roadSigns,
            Count = roadSigns.Length,
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<RoadSignDto>> GetRoadSignByIdAsync(long id)
    {
        try
        {
            var roadSign = await _roadSignRepository
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (roadSign==null)
            {
                _logger.Warning($"Дорожній знак з id: {id} не знайдено");
                return new BaseResult<RoadSignDto>()
                {
                    ErrorMessage = ErrorMessage.RoadSignNotFound,
                    ErroreCode = (int)ErrorCodes.RoadSignNotFound,
                };
            }

            return new BaseResult<RoadSignDto>()
            {
                Date = _mapper.Map<RoadSignDto>(roadSign),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<RoadSignDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<RoadSignDto>> CreateRoadSignAsync(CreateRoadSignDto dto)
    {
        try
        {
            var project = await _projectRepository
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == dto.ProjectId);

            var result = _projectValidator.ValidateOnNull(project);

            if (!result.IsSeccess)
            {
                return new BaseResult<RoadSignDto>()
                {
                    ErrorMessage = ErrorMessage.ProjectNotFound,
                    ErroreCode = (int) ErrorCodes.ProjectNotFound,
                };
            }

            var roadSign = new RoadSign()
            {
                Positioning = dto.Positioning,
                PlacementOnRoad = dto.PlacementOnRoad,
                NumberOfRacks = dto.NumberOfRacks,
                ProjectId = dto.ProjectId
            };

            await _roadSignRepository.CreateAsync(roadSign);
            await _projectRepository.SaveChangesAsync();

            return new BaseResult<RoadSignDto>()
            {
                Date = _mapper.Map<RoadSignDto>(roadSign)
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<RoadSignDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<RoadSignDto>> DeleteRoadSignAsync(long id)
    {
        try
        {
            var roadSign = await _roadSignRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (roadSign == null)
            {
                return new BaseResult<RoadSignDto>()
                {
                    ErrorMessage = ErrorMessage.RoadSignNotFound,
                    ErroreCode = (int) ErrorCodes.RoadSignNotFound,
                };
            }

            _roadSignRepository.Remove(roadSign);
            await _roadSignRepository.SaveChangesAsync();

            return new BaseResult<RoadSignDto>()
            {
                Date = _mapper.Map<RoadSignDto>(roadSign),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<RoadSignDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<RoadSignDto>> UpdateRoadSignAsync(UpdateRoadSignDto dto)
    {
        try
        {
            var roadSign = await _roadSignRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (roadSign==null)
            {
                return new BaseResult<RoadSignDto>()
                {
                    ErrorMessage = ErrorMessage.RoadSignNotFound,
                    ErroreCode = (int)ErrorCodes.RoadSignNotFound,
                };
            }

            roadSign.Positioning = dto.Positioning;
            roadSign.PlacementOnRoad = dto.PlacementOnRoad;
            roadSign.NumberOfRacks = dto.NumberOfRacks;

            _roadSignRepository.Update(roadSign);
            await _roadSignRepository.SaveChangesAsync();

            return new BaseResult<RoadSignDto>()
            {
                Date = _mapper.Map<RoadSignDto>(roadSign),
            };
        }
        catch (Exception e)
        {
            _logger.Error(e,e.Message);
            return new BaseResult<RoadSignDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
        
    }
}