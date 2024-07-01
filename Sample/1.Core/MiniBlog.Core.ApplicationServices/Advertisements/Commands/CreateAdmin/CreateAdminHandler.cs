using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Parameters.Admins;
using MiniBlog.Core.RequestResponse.Admins.Commands.Create;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.CreateAdmin;

public class CreateAdminHandler : CommandHandler<AdminCreateCommand, long>
{
    private readonly IAdvertisementCommandRepository repository;
    public CreateAdminHandler(UtilitiesServices utilitiesServices, IAdvertisementCommandRepository repository) : base(utilitiesServices)
    {
        this.repository = repository;
    }

    public override async Task<CommandResult<long>> Handle(AdminCreateCommand command)
    {
        var entity = UtilitiesServices.MapperFacade.Map<AdminCreateCommand, AdminCreateParameter>(command);
        var result = repository.CreateAdmin(new Domain.Advertisements.Entities.Admin(entity));
        await repository.CommitAsync();
        return await OkAsync(result);
    }
}
