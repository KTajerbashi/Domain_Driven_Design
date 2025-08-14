using BaseSource.EndPoint.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Controllers.Test;

public class ProviderController : BaseController
{
    
    [HttpGet]
    public IActionResult Get()
    {
       var result = Factory.JsonSerializer.Serialize(new ModelTestJson()
        {
            Name = "Kamran",
            Family = "Kamrani",
            EndDate = DateTime.Now,
        });
        return Ok(new
        {
            json = result,
            model = Factory.JsonSerializer.Deserialize<ModelTestJson>(result)
        });
    }
}
