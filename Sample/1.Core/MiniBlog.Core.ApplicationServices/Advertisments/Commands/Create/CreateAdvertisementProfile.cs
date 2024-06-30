using AutoMapper;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.RequestResponse.Advertisments.Commands.Create;
namespace MiniBlog.Core.ApplicationServices.Advertisments.Commands.Create;

public class CreateAdvertisementProfile : Profile
{
    public CreateAdvertisementProfile()
    {
        CreateMap<AdvertisementCreateCommand,AdvertisementCreateParameter>().ReverseMap();
    }
}
