using System.Xml.Linq;
using AutoMapper;
using Domain.DorZnakUA.Dto.WindZone;
using Domain.DorZnakUA.Entity;

namespace DorZnakUA.Application.Mapping;

public class WindZoneMapping : Profile
{
    public WindZoneMapping()
    {
        CreateMap<WindZone, WindZoneDto>()
            .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(x => x.Id))
            .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(x => x.Name));
    }
    
}