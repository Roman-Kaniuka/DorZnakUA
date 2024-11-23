using AutoMapper;
using Domain.DorZnakUA.Dto.WindZone;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DorZnakUA.Application.Services;

public class WindZoneService : IWindZoneService
{
    private readonly IBaseRepository<WindZone> _windZoneRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    public WindZoneService(IBaseRepository<WindZone> windZoneRepository, ILogger logger, IMapper mapper)
    {
        _windZoneRepository = windZoneRepository;
        _logger = logger;
        _mapper = mapper;
    }

    /// <inheritdoc/>>
    public async Task<CollectionResult<WindZoneDto>> GetAllWindZonesAsync()
    {
        WindZoneDto[] windZones;
        
        try
        {
            windZones = await _windZoneRepository
                .GetAll()
                .AsNoTracking()
                .Select(x => new WindZoneDto(x.Id, x.Name))
                .ToArrayAsync();
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new CollectionResult<WindZoneDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }

        if (!windZones.Any())
        {
            _logger.Warning($"Вітрових районів не існує.");
            return new CollectionResult<WindZoneDto>()
            {
                ErrorMessage = ErrorMessage.WindZonesNotFound,
                ErroreCode = (int)ErrorCodes.WindZonesNotFound,
            };
        }

        return new CollectionResult<WindZoneDto>()
        {
            Date = windZones,
            Count = windZones.Length,
        };
    }

    /// <inheritdoc/>>
    public async Task<BaseResult<WindZoneDto>> GetWindZoneByIdAsync(long id)
    {
        try
        {
            var windZone = await _windZoneRepository
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (windZone==null)
            {
                _logger.Warning($"Вітрова зона з id:{id} не знайдено.");
                return new BaseResult<WindZoneDto>()
                {
                    ErrorMessage = ErrorMessage.WindZoneNotFound,
                    ErroreCode = (int) ErrorCodes.WindZoneNotFound,
                };
            }

            return new BaseResult<WindZoneDto>()
            {
                Date = _mapper.Map<WindZoneDto>(windZone),
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<WindZoneDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>>
    public async Task<BaseResult<WindZoneDto>> CreateWindZoneAsync(CreateWindZoneDto dto)
    {
        try
        {
            var windZone = await _windZoneRepository
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == dto.Name);

            if (windZone!=null)
            {
                _logger.Warning($"Вітрова зона з назвою:{dto.Name} вже існує.");
                return new BaseResult<WindZoneDto>()
                {
                    ErrorMessage = ErrorMessage.WindZoneAlreadyExists,
                    ErroreCode = (int) ErrorCodes.WindZoneAlreadyExists,
                };
            }

            windZone = new WindZone()
            {
                Name = dto.Name,
                Description = dto.Description,
            };

            await _windZoneRepository.CreateAsync(windZone);
            await _windZoneRepository.SaveChangesAsync();

            return new BaseResult<WindZoneDto>()
            {
                Date = _mapper.Map<WindZoneDto>(windZone),
            };

        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<WindZoneDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>>
    public async Task<BaseResult<WindZoneDto>> DeleteWindZoneAsync(long id)
    {
        try
        {
            var windZone = await _windZoneRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (windZone==null)
            {
                _logger.Warning($"Вітровий район з id:{id} не знайдено.");
                return new BaseResult<WindZoneDto>()
                {
                    ErrorMessage = ErrorMessage.WindZoneNotFound,
                    ErroreCode = (int) ErrorCodes.WindZoneNotFound,
                };
            }

            _windZoneRepository.Remove(windZone);
            await _windZoneRepository.SaveChangesAsync();

            return new BaseResult<WindZoneDto>()
            {
                Date = _mapper.Map<WindZoneDto>(windZone),
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<WindZoneDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>>
    public async Task<BaseResult<WindZoneDto>> UpdateWindZoneAsync(UpdateWindZoneDto dto)
    {
        try
        {
            var windZone = await _windZoneRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (windZone==null)
            {
                _logger.Warning($"Вітровий район з id:{dto.Id} не знайдено.");
                return new BaseResult<WindZoneDto>()
                {
                    ErrorMessage = ErrorMessage.WindZoneNotFound,
                    ErroreCode = (int) ErrorCodes.WindZoneNotFound,
                };
            }

            windZone.Name = dto.Name;
            windZone.Description = dto.Description;

            _windZoneRepository.Update(windZone);
            await _windZoneRepository.SaveChangesAsync();

            return new BaseResult<WindZoneDto>()
            {
                Date = _mapper.Map<WindZoneDto>(windZone),
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<WindZoneDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }
}