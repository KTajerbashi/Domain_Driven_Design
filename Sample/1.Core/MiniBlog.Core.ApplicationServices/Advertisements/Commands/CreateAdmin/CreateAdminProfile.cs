using AutoMapper;
using MiniBlog.Core.Domain.Advertisements.Parameters.Admins;
using MiniBlog.Core.RequestResponse.Admins.Commands.Create;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.CreateAdmin;

public class CreateAdminProfile : Profile
{
    public CreateAdminProfile()
    {
        CreateMap<AdminCreateCommand, AdminCreateParameter>().ReverseMap();
    }
}
