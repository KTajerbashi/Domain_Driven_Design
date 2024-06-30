using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.Contracts.Library.Data.Commands;
using DDD.Core.Domain.Library.Exceptions;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.RequestResponse.Advertisments.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.ApplicationServices.Advertisments.Commands.Update;

public class AdvertisementUpdateHandler : CommandHandler<AdvertisementUpdateCommand>
{
    private readonly IAdvertisementCommandRepository _advertisementCommandRepository;
    public AdvertisementUpdateHandler(UtilitiesServices utilitiesServices, IAdvertisementCommandRepository advertisementCommandRepository) : base(utilitiesServices)
    {
        _advertisementCommandRepository = advertisementCommandRepository;
    }

    public override async Task<CommandResult> Handle(AdvertisementUpdateCommand command)
    {
        Advertisement entity = await _advertisementCommandRepository.GetAsync(command.Id);

        if (entity is null)
        {
            throw new InvalidEntityStateException("VALIDATION_ERROR_NOT_EXIST", nameof(entity));
        }
        var param = UtilitiesServices.MapperFacade.Map<AdvertisementUpdateCommand, AdvertisementUpdateParameter>(command);
        entity.Update(param);

        await _advertisementCommandRepository.CommitAsync();

        return Ok();
    }
}
