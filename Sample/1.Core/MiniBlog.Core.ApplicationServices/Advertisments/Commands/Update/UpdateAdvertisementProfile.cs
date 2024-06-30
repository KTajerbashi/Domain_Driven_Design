using AutoMapper;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.RequestResponse.Advertisments.Commands.Update;

namespace MiniBlog.Core.ApplicationServices.Advertisments.Commands.Update;

public class UpdateAdvertisementProfile : Profile
{
    public UpdateAdvertisementProfile()
    {
        CreateMap<AdvertisementUpdateCommand, AdvertisementUpdateParameter>().ReverseMap();
    }
}