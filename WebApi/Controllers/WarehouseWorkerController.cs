using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public abstract class WareHouseWorkerController : CommonController
    {
        protected WareHouseWorkerController(IWarehouseCustomersService warehouseCustomersService) : base(warehouseCustomersService) { }

        [HttpGet]
        [Route("/orders")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetOrderList(GetOrderListRequestModel getOrdersRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = WarehouseCustomersService.CheckRequest(getOrdersRequestModel, AccessRights.Worker);
                if (error is not null) return BadRequest(error);
                GetOrderListSuccessModel response = WarehouseCustomersService.GetOrderList(getOrdersRequestModel);
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
