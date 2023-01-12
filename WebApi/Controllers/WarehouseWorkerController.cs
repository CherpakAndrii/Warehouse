using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class WareHouseWorkerController : ControllerBase
    {
        private readonly IWarehouseUserService _warehouseUserService;

        public WareHouseWorkerController(IWarehouseUserService warehouseUserService)
        {
            _warehouseUserService = warehouseUserService;
        }

        [HttpGet]
        [Route("/orders")]
        public IActionResult GetOrderList([FromQuery]GetOrderListRequestModel getOrdersRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(getOrdersRequestModel, AccessRights.Worker);
                if (error is not null) return BadRequest(error);
                GetOrderListSuccessModel response = _warehouseUserService.GetOrderList(getOrdersRequestModel);
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
