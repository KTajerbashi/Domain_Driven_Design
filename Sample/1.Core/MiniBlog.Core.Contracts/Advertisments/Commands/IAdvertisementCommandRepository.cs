using DDD.Core.Contracts.Library.Data.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.Contracts.Advertisments.Commands;

public interface IAdvertisementCommandRepository:ICommandRepository<Advertisement,long>
{
}
