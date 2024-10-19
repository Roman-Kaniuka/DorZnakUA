using System.Reflection.PortableExecutable;
using AutoMapper;
using Domain.DorZnakUA.Dto.Shield;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DorZnakUA.Application.Services;

public class ShieldService : IShieldService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Shield> _shieldRepository;
    private readonly IBaseRepository<RoadSign> _roadSignRepository;

    public ShieldService(ILogger logger, IBaseRepository<Shield> shieldRepository, IMapper mapper,
        IBaseRepository<RoadSign> roadSignRepository)
    {
        _logger = logger;
        _shieldRepository = shieldRepository;
        _mapper = mapper;
        _roadSignRepository = roadSignRepository;
    }

    /// <inheritdoc/>
    public async Task<CollectionResult<ShieldDto>> GetAllShieldsAsync()
    {
        try
        {
            var shields = await _shieldRepository
                .GetAll()
                .Select(x=> new ShieldDto(x.Id, x.Name, x.SizeType))
                .ToArrayAsync();

            if (!shields.Any())
            {
                _logger.Warning("Дорожніх щитів не існує.");
                return new CollectionResult<ShieldDto>()
                {
                    ErrorMessage = ErrorMessage.ShieldsNotFound,
                    ErroreCode = (int) ErrorCodes.ShieldsNotFound,
                };
            }

            return new CollectionResult<ShieldDto>()
            {
                Date = shields,
                Count = shields.Length
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new CollectionResult<ShieldDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ShieldDto>> GetShieldByIdAsync(long id)
    {
        try
        {
            var shield = await _shieldRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (shield==null)
            {
                _logger.Warning($"Щит з id:{id} не знайдено.");
                return new BaseResult<ShieldDto>()
                {
                    ErrorMessage = ErrorMessage.ShieldNotFound,
                    ErroreCode = (int)ErrorCodes.ShieldNotFound,
                };
            }

            return new BaseResult<ShieldDto>()
            {
                Date = _mapper.Map<ShieldDto>(shield),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<ShieldDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    public async Task<CollectionResult<ShieldDto>> GetRoadSignShieldsAsync(long roadSignId)
    {
        try
        {
            var roadSign = await _roadSignRepository
                .GetAll()
                .Include(x=>x.Shields)
                .FirstOrDefaultAsync(x => x.Id == roadSignId);
            
            if (roadSign==null)
            {
                _logger.Warning($"Дорожній знак з id:{roadSignId} не знайдено.");
                return new CollectionResult<ShieldDto>()
                {
                    ErrorMessage = ErrorMessage.RoadSignNotFound,
                    ErroreCode = (int)ErrorCodes.RoadSignNotFound,
                };
            }

            var roadSignShields = roadSign.Shields
                .Select(x=>new ShieldDto(x.Id,x.Name,x.SizeType))
                .ToArray();

            if (!roadSignShields.Any())
            {
                _logger.Warning($"Дорожній знак з id:{roadSignId} не має щитів.");
                return new CollectionResult<ShieldDto>()
                {
                    ErrorMessage = ErrorMessage.ShieldsNotFound,
                    ErroreCode = (int)ErrorCodes.ShieldsNotFound,
                };
            }

            return new CollectionResult<ShieldDto>()
            {
                Date = roadSignShields,
                Count = roadSignShields.Length,
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new CollectionResult<ShieldDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ShieldDto>> CreateShieldAsync(CreateShieldDto dto)
    {
        try
        {
            var shields = await _shieldRepository
                .GetAll()
                .ToArrayAsync();

            var newShield = new Shield()
            {
                Group = dto.Group,
                Name = dto.Name,
                Shape = dto.Shape,
                SizeType = dto.SizeType,
                Height = dto.Height,
                Weight = dto.Weight,
                Width = dto.Width
            };

            foreach (var shield in shields)
            {
                if (shield.Equals(newShield))
                {
                    _logger.Warning($"Щит з назвою:{newShield.Name} та типорозміром:{newShield.SizeType} вже існує.");
                    return new BaseResult<ShieldDto>()
                    {
                        ErrorMessage = ErrorMessage.ShieldAlreadyExists,
                        ErroreCode = (int) ErrorCodes.ShieldAlreadyExists,
                    };
                }
            }

            await _shieldRepository.CreateAsync(newShield);
            await _shieldRepository.SaveChangesAsync();

            return new BaseResult<ShieldDto>()
            {
                Date = _mapper.Map<ShieldDto>(newShield),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<ShieldDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ShieldDto>> DeleteShieldAsync(long id)
    {
        try
        {
            var shield = await _shieldRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (shield==null)
            {
                _logger.Warning($"Щит з id:{id} не знайдено");
                return new BaseResult<ShieldDto>()
                {
                    ErrorMessage = ErrorMessage.ShieldNotFound,
                    ErroreCode = (int) ErrorCodes.ShieldNotFound,
                };
            }

            _shieldRepository.Remove(shield);
            await _shieldRepository.SaveChangesAsync();

            return new BaseResult<ShieldDto>()
            {
                Date = _mapper.Map<ShieldDto>(shield),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<ShieldDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ShieldDto>> UpdateShieldDto(UpdateShieldDto dto)
    {
        try
        {
            var shield = await _shieldRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (shield==null)
            {
                _logger.Warning($"Щит з id:{dto.Id} не знайдено");
                return new BaseResult<ShieldDto>()
                {
                    ErrorMessage = ErrorMessage.ShieldNotFound,
                    ErroreCode = (int) ErrorCodes.ShieldNotFound,
                };
            }

            var shields = await _shieldRepository
                .GetAll()
                .ToArrayAsync();

            shield.Group = dto.Group;
            shield.Name = dto.Name;
            shield.Shape = dto.Shape;
            shield.SizeType = dto.SizeType;
            shield.Height = dto.Height;
            shield.Weight = dto.Weight;
            shield.Width = dto.Width;

            foreach (var s in shields)
            {
                if (s.Equals(shield))
                {
                    _logger.Warning($"Щит з назвою:{shield.Name} та типорозміром:{shield.SizeType} вже існує.");
                    return new BaseResult<ShieldDto>()
                    {
                        ErrorMessage = ErrorMessage.ShieldAlreadyExists,
                        ErroreCode = (int) ErrorCodes.ShieldAlreadyExists,
                    };
                }
            }

            _shieldRepository.Update(shield);
            await _shieldRepository.SaveChangesAsync();

            return new BaseResult<ShieldDto>()
            {
                Date = _mapper.Map<ShieldDto>(shield),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<ShieldDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int) ErrorCodes.InternalServerError,
            };
        }
    }
}