using AutoMapper;

namespace BaseSource.EndPoint.WebApi.Controllers.Test;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ModelTestJson, ModelTestJsonDTO>().ReverseMap();
    }
}

