using DDD.EndPoints.Web.Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.RequestResponse.People.Commands.Create;

namespace MiniBlog.EndPoints.API.Controllers.PersosContainer
{
    [Route("api/[controller]")]
    public class PersonController : BaseController
    {
        [HttpPost("/CreatePerson")]
        public async Task<IActionResult> CreatePerson(CreatePerson createPerson) 
        {
            return await Create<CreatePerson,int>(createPerson);
        }
    }
}
