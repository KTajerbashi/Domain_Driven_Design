using DDD.EndPoints.Web.Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.RequestResponse.Courses.Commands.Create;
using MiniBlog.Core.RequestResponse.Courses.Commands.Update;
using MiniBlog.Core.RequestResponse.Courses.Commands.Delete;

namespace MiniBlog.EndPoints.API.Controllers.Advertisements;

public sealed class CourseController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CourseCreateCommand command)
        => await Create<CourseCreateCommand, long>(command);

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] CourseUpdateCommand command)
        => await Edit(command);

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] CourseDeleteCommand command)
        => await Delete(command);
}
