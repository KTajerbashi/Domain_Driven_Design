using BaseSource.EndPoint.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Controllers.Test;

public class ModelTestJson
{
    public string Name { get; set; }
    public string Family { get; set; }
    public DateTime EndDate { get; set; }
}

public class ProviderController : BaseController
{

    [HttpGet("JsonSerializer")]
    public IActionResult JsonSerializer()
    {
        var result = Factory.Serializer.Serialize(new ModelTestJson()
        {
            Name = "Kamran",
            Family = "Kamrani",
            EndDate = DateTime.Now,
        });
        return Ok(new
        {
            json = result,
            model = Factory.Serializer.Deserialize<ModelTestJson>(result)
        });
    }

    [HttpGet("CacheAdapter/{key}/{value}")]
    public IActionResult CacheAdapter(string key,string value)
    {
        Factory.Cache.Add($"{key}", $"{value}", DateTime.Now.AddHours(5), TimeSpan.FromHours(5));
        var result = Factory.Cache.Get<string>(key);
        Factory.Cache.RemoveCache(key);

        return Ok(new
        {
            data = result
        });
    }
}
