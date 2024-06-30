using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.Domain.Library.Exceptions;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.UnPublishe;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.UnPublishe;

public class AdvertisementUnPublishHandler : CommandHandler<AdvertisementUnPublishCommand>
{
    private readonly IAdvertisementCommandRepository _commandRepository;
    public AdvertisementUnPublishHandler(UtilitiesServices utilitiesServices, IAdvertisementCommandRepository commandRepository) : base(utilitiesServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(AdvertisementUnPublishCommand command)
    {
        Advertisement entity = await _commandRepository.GetAsync(command.Id);

        if (entity is null)
        {
            throw new InvalidEntityStateException("VALIDATION_ERROR_NOT_EXIST", nameof(entity));
        }

        entity.UnPublish();

        await _commandRepository.CommitAsync();

        return Ok();
    }
}
