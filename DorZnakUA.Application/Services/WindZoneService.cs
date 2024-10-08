using Domain.DorZnakUA.Dto.WindZone;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;

namespace DorZnakUA.Application.Services;

public class WindZoneService : IWindZoneService
{
    
    /// <inheritdoc/>>
    public Task<CollectionResult<WindZoneDto>> GetAllWindZonesAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>>
    public Task<BaseResult<WindZoneDto>> GetWindZoneByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>>
    public Task<BaseResult<WindZoneDto>> CreateWindZoneAsync(CreateWindZoneDto dto)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>>
    public Task<BaseResult<WindZoneDto>> DeleteWindZoneAsync(long id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>>
    public Task<BaseResult<WindZoneDto>> UpdateWindZoneAsync(UpdateWindZoneDto dto)
    {
        throw new NotImplementedException();
    }
}