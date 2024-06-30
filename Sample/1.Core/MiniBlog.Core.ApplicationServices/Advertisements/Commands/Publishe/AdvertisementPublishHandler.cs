using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.Domain.Library.Exceptions;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Delete;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Publishe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.Publishe;

public class AdvertisementPublishHandler : CommandHandler<AdvertisementPublishCommand>
{
    private readonly IAdvertisementCommandRepository _commandRepository;
    public AdvertisementPublishHandler(UtilitiesServices utilitiesServices, IAdvertisementCommandRepository commandRepository) : base(utilitiesServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(AdvertisementPublishCommand command)
    {
        Advertisement entity = await _commandRepository.GetAsync(command.Id);

        if (entity is null)
            throw new InvalidEntityStateException("VALIDATION_ERROR_NOT_EXIST", nameof(entity));

        entity.Publish();

        await _commandRepository.CommitAsync();

        return Ok();
    }
}
