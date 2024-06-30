using AutoMapper;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Create;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.Create;

public class CreateAdvertisementProfile : Profile
{
    public CreateAdvertisementProfile()
    {
        CreateMap<AdvertisementCreateCommand, AdvertisementCreateParameter>().ReverseMap();
    }
}
