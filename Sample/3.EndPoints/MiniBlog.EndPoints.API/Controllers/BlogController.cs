using DDD.EndPoints.Web.Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.RequestResponse.People.Commands.Create;

namespace MiniBlog.EndPoints.API.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : BaseController
    {
        private readonly ILogger<BlogController> logger;
        public BlogController(ILogger<BlogController> logger)
        {
            this.logger = logger;
        }
        #region Test Methods
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePerson(CreatePerson createPerson)
        {
            try
            {
                //Person
                return Ok(ModelState);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePerson()
        {
            return Ok(ModelState);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePerson()
        {
            return Ok(ModelState);
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetPerson()
        {
            return Ok(ModelState);
        }
        [HttpGet("/Clear")]
        public bool Clear()
        {
            GC.Collect(2);
            return true;
        }
        #endregion


        #region Commands
        //[HttpPost("Create")]
        //public async Task<IActionResult> CreateBlog([FromBody] CreateBlogCommand command) => await Create<CreateBlogCommand, Guid>(command);

        //[HttpPut("Update")]
        //public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogCommand command) => await Edit(command);

        //[HttpDelete("Delete")]
        //public async Task<IActionResult> DeleteBlog([FromBody] DeleteBlogCommand command) => await Delete(command);

        //[HttpDelete("DeleteGraph")]
        //public async Task<IActionResult> DeleteGraphBlog([FromBody] DeleteGraphBlogCommand command) => await Delete(command);

        //[HttpPost("AddPost")]
        //public async Task<IActionResult> AddPost([FromBody] AddPostCommand command) => await Create(command);

        //[HttpDelete("RemovePost")]
        //public async Task<IActionResult> RemovePost([FromBody] RemovePostCommand command) => await Delete(command);
        #endregion

        #region Queries
        //[HttpGet("GetById")]
        //public async Task<IActionResult> GetById(GetBlogByIdQuery query) => await Query<GetBlogByIdQuery, BlogQr?>(query);
        #endregion

        #region Methods
        //[HttpGet("/Clear")]
        //public bool Clear()
        //{
        //    GC.Collect(2);
        //    return true;
        //}
        #endregion
    }
}