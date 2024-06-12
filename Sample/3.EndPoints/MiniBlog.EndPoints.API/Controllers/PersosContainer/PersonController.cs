using DDD.EndPoints.Web.Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.RequestResponse.People.Commands.Create;

namespace MiniBlog.EndPoints.API.Controllers.PersosContainer
{
    [Route("api/[controller]")]
    public class PersonController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createPerson"></param>
        /// <returns></returns>
        [HttpPost("/CreatePerson")]
        public async Task<IActionResult> CreatePerson(CreatePerson createPerson) => await Create<CreatePerson, int>(createPerson);
    }
}
