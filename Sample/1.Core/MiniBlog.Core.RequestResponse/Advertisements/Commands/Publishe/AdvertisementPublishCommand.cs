using DDD.Core.RequestResponse.Library.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.RequestResponse.Advertisements.Commands.Publishe;

public sealed class AdvertisementPublishCommand : ICommand
{
    public long Id { get; set; }
}
