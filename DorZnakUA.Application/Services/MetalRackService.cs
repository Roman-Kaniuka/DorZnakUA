using Domain.DorZnakUA.Dto.MetalRack;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;

namespace DorZnakUA.Application.Services;

public class MetalRackService : IMetalRackService
{
    /// <inheritdoc/>
    public Task<CollectionResult<MetalRackDto>> GetMetalRacksAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<BaseResult<MetalRackDto>> GetMetalRackByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<BaseResult<MetalRackDto>> GetMetalRacksAsync(long roadSignId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<BaseResult<MetalRackDto>> CreateMetalRackAsync(CreateMetalRackDto dto)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<BaseResult<MetalRackDto>> DeleteMetalRack(long id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<BaseResult<MetalRackDto>> UpdateMetalRack(UpdateMetalRackDto dto)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<BaseResult<MetalRackDto>> CalculateRackHeightAsync(RoadSign roadSign)
    {
        throw new NotImplementedException();
    }
}