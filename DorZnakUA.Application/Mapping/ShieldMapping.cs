using AutoMapper;
using Domain.DorZnakUA.Dto.Shield;
using Domain.DorZnakUA.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DorZnakUA.Application.Mapping;

public class ShieldMapping : Profile
{
    public ShieldMapping()
    {
        CreateMap<Shield, ShieldDto>()
            .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(x => x.Id))
            .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(x => x.Name))
            .ForCtorParam(ctorParamName: "SizeType", m => m.MapFrom(x => x.SizeType));
    }
}