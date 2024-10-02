using System.Xml.Linq;
using AutoMapper;
using Domain.DorZnakUA.Dto.MetalRack;
using Domain.DorZnakUA.Entity;

namespace DorZnakUA.Application.Mapping;

public class MetalRackMapping : Profile
{
    public MetalRackMapping()
    {
        CreateMap<MetalRack, MetalRackDto>()
            .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(x => x.Id))
            .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(x => x.Name))
            .ForCtorParam(ctorParamName: "Height", m => m.MapFrom(x => x.Height))
            .ForCtorParam(ctorParamName: "Diameter", m => m.MapFrom(x => x.Diameter))
            .ReverseMap();
    }
}