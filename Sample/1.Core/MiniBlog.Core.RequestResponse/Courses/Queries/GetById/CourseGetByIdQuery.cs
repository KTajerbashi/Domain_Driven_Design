using DDD.Core.RequestResponse.Library.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.RequestResponse.Courses.Queries.GetById;

public sealed class CourseGetByIdQuery : IQuery<CourseQr?>
{
    public int Id { get; set; }
}
