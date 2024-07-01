using DDD.Core.RequestResponse.Library.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.RequestResponse.Admins.Queries.GetById;

public sealed class AdminGetByIdQuery : IQuery<AdminQr?>
{
    public int Id { get; set; }
}
