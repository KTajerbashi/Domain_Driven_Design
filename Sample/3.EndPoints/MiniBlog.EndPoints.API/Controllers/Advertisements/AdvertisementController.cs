using DDD.EndPoints.Web.Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Create;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Delete;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Update;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.Publishe;
using MiniBlog.Core.RequestResponse.Advertisements.Commands.UnPublishe;
using MiniBlog.Core.RequestResponse.Advertisements.Queries.GetById;

namespace MiniBlog.EndPoints.API.Controllers.Advertisements;

public sealed class AdvertisementController : BaseController
{
    #region Commands
    [HttpPost("Create")]
    public async Task<IActionResult> CreateAdvertisement([FromBody] AdvertisementCreateCommand command)
        => await Create<AdvertisementCreateCommand, long>(command);

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateAdvertisement([FromBody] AdvertisementUpdateCommand command)
        => await Edit(command);

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAdvertisement([FromBody] AdvertisementDeleteCommand command)
        => await Delete(command);

    [HttpPut("Publish")]
    public async Task<IActionResult> PublishAdvertisement([FromBody] AdvertisementPublishCommand command)
    => await Edit(command);


    [HttpPut("UnPublish")]
    public async Task<IActionResult> UnPublishAdvertisement([FromBody] AdvertisementUnPublishCommand command) => await Edit(command);
    #endregion

    #region Queries
    [HttpGet("GetById")]
    public async Task<IActionResult> GetAdvertisementById([FromQuery] AdvertisementGetByIdQuery query)
        => await Query<AdvertisementGetByIdQuery, AdvertisementQr?>(query);

    //[HttpGet("GetSelectList")]
    //public async Task<IActionResult> GetAdvertisementSelectList([FromQuery] AdvertisementGetSelectListQuery query)
    //    => await Query<AdvertisementGetSelectListQuery, List<AdvertisementSelectItemQr>>(query);

    //[HttpGet("GetPagedFilter")]
    //public async Task<IActionResult> GetAdvertisementPagedFilter([FromQuery] AdvertisementGetPagedFilterQuery query)
    //    => await Query<AdvertisementGetPagedFilterQuery, PagedData<AdvertisementListItemQr>>(query);
    #endregion
}
