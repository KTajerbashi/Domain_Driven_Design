using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.Domain.Library.Exceptions;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.RequestResponse.Advertisments.Commands.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.ApplicationServices.Advertisments.Commands.Delete;

public class AdvertisementDeleteHandler : CommandHandler<AdvertisementDeleteCommand>
{
    private readonly IAdvertisementCommandRepository _commandRepository;
    public AdvertisementDeleteHandler(UtilitiesServices utilitiesServices, IAdvertisementCommandRepository commandRepository) : base(utilitiesServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(AdvertisementDeleteCommand command)
    {
        Advertisement entity = await _commandRepository.GetAsync(command.Id);

        if (entity is null)
        {
            throw new InvalidEntityStateException("VALIDATION_ERROR_NOT_EXIST", nameof(entity));
        }

        entity.Delete();

        _commandRepository.Delete(entity);

        await _commandRepository.CommitAsync();

        return Ok();
    }
}
