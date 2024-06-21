using DDD.EndPoints.Web.Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.RequestResponse.People.Commands.Create;
using MiniBlog.Core.RequestResponse.People.Queries.GetAllPerson;
using MiniBlog.Core.RequestResponse.People.Queries.GetPersonById;

namespace MiniBlog.EndPoints.API.Controllers.People
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


        #region Queries
        [HttpGet("GetPersonById")]
        public async Task<IActionResult> GetById(GetPersonByIdModel query) => await Query<GetPersonByIdModel, PersonQuery?>(query);
        #endregion

    }
}
