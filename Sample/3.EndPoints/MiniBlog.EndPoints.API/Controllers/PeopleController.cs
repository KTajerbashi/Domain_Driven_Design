using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.Domain.People.ValueObjects;

namespace MiniBlog.EndPoints.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PeopleController> _logger;

        public PeopleController(ILogger<PeopleController> logger)
        {
            _logger = logger;
            _logger.LogInformation("PeopleController Run ...");
        }

        [HttpGet(Name = "GetValueObjectEQ")]
        public IActionResult GetValueObjectEQ()
        {
            FirstName firstName01 = "Alireza1";
            FirstName firstName02 = "Alireza";
            return Ok(firstName01 == firstName02);
        }
        [HttpGet("/GetLengthException")]
        public IActionResult GetLengthException()
        {
            try
            {
                FirstName firstName = new("a");
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpGet("/CreatePerson")]
        public IActionResult CreatePerson()
        {
            try
            {
                Person p = new(-1,"Kamran","Tajerbashi");
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }
        [HttpGet("/CreatePersonEventBase")]
        public IActionResult CreatePersonEventBase()
        {
            try
            {
                PersonEventBase p = new(1,"Kamran","Tajerbashi");
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }
    }
}
