using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Common.Response;
using Models.Api.Common.Request;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public abstract class WareHouseWorkerController : CommonController
    {
        public WareHouseWorkerController(IWarehouseCustomersService warehouseCustomersService) : base(warehouseCustomersService) { }

        [HttpGet]
        [Route("/orders")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetOrderList(GetOrderListRequestModel getOrdersRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseCustomersService.CheckRequest(getOrdersRequestModel, AccessRights.Worker);
                if (error is not null) return BadRequest(error);
                GetOrderListSuccessModel response = _warehouseCustomersService.GetOrderList(getOrdersRequestModel);
                if (response == null)
                    return StatusCode(500);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message)
                {
                    StatusCode = 500
                };
            }
        }
    }
}
