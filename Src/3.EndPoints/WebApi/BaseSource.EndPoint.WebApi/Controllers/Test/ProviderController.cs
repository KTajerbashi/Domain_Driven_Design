using AutoMapper;
using BaseSource.EndPoint.WebApi.Common.Controllers;
using BaseSource.EndPoint.WebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BaseSource.EndPoint.WebApi.Controllers.Test;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ModelTestJson, ModelTestJsonDTO>().ReverseMap();
    }
}

public class ModelTestJson
{
    public string Name { get; set; }
    public string Family { get; set; }
    public DateTime EndDate { get; set; }
}


public class ModelTestJsonDTO
{
    public string Name { get; set; }
    public string Family { get; set; }
    public DateTime EndDate { get; set; }
    public string StartDate { get; set; }
}

public class ProviderController : BaseController
{
    private readonly ILogger<ProviderController> _logger;
    public ProviderController(ILogger<ProviderController> logger)
    {
        _logger = logger;
        //_loggerFactory = Factory.LoggerFactory.CreateLogger("ProviderController");
    }

    [HttpPost("JsonSerializer")]
    public IActionResult JsonSerializer(ModelTestJson parameter)
    {
        var result = Factory.Serializer.Serialize(parameter);
        return Ok(new
        {
            json = result,
            model = Factory.Serializer.Deserialize<ModelTestJson>(result)
        });
    }

    [HttpGet("CacheAdapter/{key}/{value}")]
    public IActionResult CacheAdapter(string key, string value)
    {
        Factory.Cache.Add($"{key}", $"{value}", DateTime.Now.AddHours(5), TimeSpan.FromHours(5));
        var result = Factory.Cache.Get<string>(key);
        Factory.Cache.RemoveCache(key);

        return Ok(new
        {
            data = result
        });
    }

    [HttpGet("Mapper")]
    public IActionResult Mapper()
    {
        var result = new ModelTestJson()
        {
            Name = "Kamran",
            Family = "Kamrani",
            EndDate = DateTime.Now,
        };

        var mapped = Factory.Mapper.Map<ModelTestJson, ModelTestJsonDTO>(result);
        var reversed = Factory.Mapper.Map<ModelTestJsonDTO, ModelTestJson>(mapped);
        return Ok(new
        {
            Model = result,
            Result = mapped,
            Reversed = reversed
        });
    }

    [HttpGet("LoggerFactory")]
    public IActionResult LoggerFactory()
    {

        try
        {
            // Your business logic here
            throw new WebApiException("This Error is With Controller WebApi Handled !!!❌");

            //return Ok(new { Message = "Success" });
            return Ok();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet("User")]
    public IActionResult User()
    {
        var user = Factory.UserFactory.GetCurrentUser();

        if (!user.IsAuthenticated)
        {
            return Unauthorized();
        }

        var userInfo = new
        {
            user.DisplayName,
            user.Email,
            user.RoleName,
            user.Permissions
        };

        return Ok(userInfo);
    }
}

