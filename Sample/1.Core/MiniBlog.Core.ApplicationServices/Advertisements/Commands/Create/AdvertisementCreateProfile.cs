using AutoMapper;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.Domain.Advertisements.Parameters.Admins;
using MiniBlog.Core.Domain.Advertisements.Parameters.Courses;
using MiniBlog.Core.RequestResponse.Admins.Commands.Create;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Create;
using MiniBlog.Core.RequestResponse.Courses.Commands.Create;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.Create;

public class AdvertisementCreateProfile : Profile
{
    public AdvertisementCreateProfile()
    {
        CreateMap<AdvertisementCreateCommand, AdvertisementCreateParameter>().ReverseMap();
        CreateMap<CourseCreateCommand, CourseCreateParameter>().ReverseMap();
        CreateMap<AdminCreateCommand, AdminCreateParameter>().ReverseMap();
    }
}
