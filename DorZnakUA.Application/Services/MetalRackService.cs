using AutoMapper;
using Domain.DorZnakUA.Dto.MetalRack;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DorZnakUA.Application.Services;

public class MetalRackService : IMetalRackService
{
    private readonly IBaseRepository<MetalRack> _metalRackRepository;
    private readonly IBaseRepository<RoadSign> _roadSignRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public MetalRackService(ILogger logger, IBaseRepository<MetalRack> metalRackRepository, IMapper mapper, IBaseRepository<RoadSign> roadSignRepository)
    {
        _logger = logger;
        _metalRackRepository = metalRackRepository;
        _mapper = mapper;
        _roadSignRepository = roadSignRepository;
    }

    /// <inheritdoc/>
    public async Task<CollectionResult<MetalRackDto>> GetAllMetalRacksAsync()
    {
        MetalRackDto[] metalRacks;
        
        try
        {
            metalRacks = await _metalRackRepository
                .GetAll()
                .AsNoTracking()
                .Select(x => new MetalRackDto(x.Id, x.Name, x.Height, x.Diameter))
                .ToArrayAsync();
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new CollectionResult<MetalRackDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
        
        if (!metalRacks.Any())
        {
            _logger.Warning($"Стійок не існує.");

            return new CollectionResult<MetalRackDto>()
            {
                ErrorMessage = ErrorMessage.MetalRacksNotFound,
                ErroreCode = (int)ErrorCodes.MetalRacksNotFound,
            };
        }

        return new CollectionResult<MetalRackDto>()
        {
            Date = metalRacks,
            Count = metalRacks.Length
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<MetalRackDto>> GetMetalRackByIdAsync(long id)
    {
        try
        {
            var metalRack = await _metalRackRepository
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (metalRack==null)
            {
                _logger.Warning($"Стійку з id:{id} не знайдено");
                return new BaseResult<MetalRackDto>()
                {
                    ErrorMessage = ErrorMessage.MetalRackNotFound,
                    ErroreCode = (int)ErrorCodes.MetalRackNotFound,
                };
            }

            return new BaseResult<MetalRackDto>()
            {
                Date = _mapper.Map<MetalRackDto>(metalRack),
            };

        }
        catch (Exception e)
        {
            _logger.Error(e,e.Message);
            return new BaseResult<MetalRackDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
       
    }

    /// <inheritdoc/>
    public async Task<BaseResult<MetalRackDto>> GetRoadSignMetalRackAsync(long roadSignId)
    {
        try
        {
            var roadSign = await _roadSignRepository
                .GetAll()
                .Include(x => x.MetalRack)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == roadSignId);

            if (roadSign==null)
            {
                _logger.Warning($"Дорожнього знаку з {roadSignId} не знайдено.");
                return new BaseResult<MetalRackDto>()
                {
                    ErrorMessage = ErrorMessage.RoadSignNotFound,
                    ErroreCode = (int)ErrorCodes.RoadSignNotFound,
                };
            }

            var metalRack = roadSign.MetalRack;

            if (metalRack==null)
            {
                _logger.Warning($"Дорожінй знак з id:{roadSignId} не  має стійки");
                return new BaseResult<MetalRackDto>()
                {
                    ErrorMessage = ErrorMessage.MetalRackNotFound,
                    ErroreCode = (int) ErrorCodes.MetalRackNotFound,
                };
            }

            return new BaseResult<MetalRackDto>()
            {
                Date = _mapper.Map<MetalRackDto>(metalRack)
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<MetalRackDto>()
            {
                ErrorMessage = ErrorMessage.MetalRackNotFound,
                ErroreCode = (int)ErrorCodes.MetalRackNotFound,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<MetalRackDto>> CreateMetalRackAsync(CreateMetalRackDto dto)
    {
        try
        {
            var metalRacks = await _metalRackRepository
                .GetAll()
                .AsNoTracking()
                .ToArrayAsync();

            var metalRackNames = metalRacks.Select(x => x.Name);

            var metalRack = new MetalRack()
            {
                Name = dto.Name,
                Height = dto.Height,
                Diameter = dto.Diameter,
                Thickness = dto.Thickness,
                Weight = dto.Weight
            };
            
            if (metalRackNames.Contains(dto.Name))
            {
                _logger.Warning($"Стійка {dto.Name} вже існує.");
                return new BaseResult<MetalRackDto>()
                {
                    ErrorMessage = ErrorMessage.MetalRackAlreadyExists,
                    ErroreCode = (int)ErrorCodes.MetalRackAlreadyExists,
                };
            }
            
            foreach (var rack in metalRacks)
            {
                if (rack.Equals(metalRack))
                {
                    _logger.Warning($"Металева стійка зі схожими параметрами вже існує.");

                    return new BaseResult<MetalRackDto>()
                    {
                        ErrorMessage = ErrorMessage.MetalRackWithSimilarParametersAlreadyExists,
                        ErroreCode = (int)ErrorCodes.MetalRackWithSimilarParametersAlreadyExists,
                    };
                }
            }

            await _metalRackRepository.CreateAsync(metalRack);
            await _metalRackRepository.SaveChangesAsync();

            return new BaseResult<MetalRackDto>()
            {
                Date = _mapper.Map<MetalRackDto>(metalRack),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<MetalRackDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<MetalRackDto>> DeleteMetalRackAsync(long id)
    {
        try
        {
            var metalRack = await _metalRackRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (metalRack==null)
            {
                _logger.Warning($"Стійку з id: {id} не знайдено.");
                return new BaseResult<MetalRackDto>()
                {
                    ErrorMessage = ErrorMessage.MetalRackNotFound,
                    ErroreCode = (int) ErrorCodes.MetalRackNotFound,
                };
            }

            _metalRackRepository.Remove(metalRack);
            await _metalRackRepository.SaveChangesAsync();

            return new BaseResult<MetalRackDto>()
            {
                Date = _mapper.Map<MetalRackDto>(metalRack),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<MetalRackDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<MetalRackDto>> UpdateMetalRackAsync(UpdateMetalRackDto dto)
    {
        try
        {
            var metalRack = await _metalRackRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (metalRack==null)
            {
                _logger.Warning($"Стійку з id: {dto.Id} не знайдено.");
                return new BaseResult<MetalRackDto>()
                {
                    ErrorMessage = ErrorMessage.MetalRackNotFound,
                    ErroreCode = (int) ErrorCodes.MetalRackNotFound,
                };
            }

            metalRack.Name = dto.Name;
            metalRack.Height = dto.Height;
            metalRack.Weight = dto.Weight;
            metalRack.Diameter = dto.Diameter;
            metalRack.Thickness = dto.Thickness;

            _metalRackRepository.Update(metalRack);
            await _metalRackRepository.SaveChangesAsync();

            return new BaseResult<MetalRackDto>()
            {
                Date = _mapper.Map<MetalRackDto>(metalRack),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<MetalRackDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public Task<BaseResult<MetalRackDto>> CalculateRackHeightAsync(RoadSign roadSign)
    {
        throw new NotImplementedException();
    }
}