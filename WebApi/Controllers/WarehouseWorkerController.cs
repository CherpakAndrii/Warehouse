using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Common.Response;
using Models.Api.Common.Request;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public abstract class WareHouseWorkerController : CommonController
    {
        public WareHouseWorkerController(IWarehouseCustomersService warehouseCustomersService) : base(warehouseCustomersService) { }

        [HttpPost]
        [Route("/orders")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetOrderList(GetOrderListRequestModel getOrdersRequestModel)
        {
            try
            {
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
