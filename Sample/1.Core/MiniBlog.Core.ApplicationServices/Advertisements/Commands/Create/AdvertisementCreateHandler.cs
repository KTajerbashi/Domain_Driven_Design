using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Create;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.Create;

public class AdvertisementCreateHandler : CommandHandler<AdvertisementCreateCommand, long>
{
    private readonly IAdvertisementCommandRepository repository;
    public AdvertisementCreateHandler(UtilitiesServices utilitiesServices, IAdvertisementCommandRepository repository) : base(utilitiesServices)
    {
        this.repository = repository;
    }

    public override async Task<CommandResult<long>> Handle(AdvertisementCreateCommand command)
    {
        var parameters = UtilitiesServices.MapperFacade.Map<AdvertisementCreateCommand,AdvertisementCreateParameter>(command);
        Advertisement entity = new(parameters);
        await repository.InsertAsync(entity);
        await repository.CommitAsync();
        return await OkAsync(entity.Id);
    }
}
