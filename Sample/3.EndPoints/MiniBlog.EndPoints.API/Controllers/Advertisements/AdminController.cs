using DDD.EndPoints.Web.Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.RequestResponse.Admins.Commands.Create;
using MiniBlog.Core.RequestResponse.Admins.Commands.Update;
using MiniBlog.Core.RequestResponse.Admins.Commands.Delete;

namespace MiniBlog.EndPoints.API.Controllers.Advertisements;

public sealed class AdminController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] AdminCreateCommand command)
        => await Create<AdminCreateCommand, long>(command);

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] AdminUpdateCommand command)
        => await Edit(command);

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] AdminDeleteCommand command)
        => await Delete(command);
}
