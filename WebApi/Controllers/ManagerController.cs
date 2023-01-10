using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Req_Res.Common.Response;
using Models.Api.Req_Res.Manager.Request;
using Models.Api.Req_Res.Manager.Response;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IWarehouseManagerService _warehouseManagerService;
        private readonly IWarehouseUserService _warehouseUserService;
        public ManagerController(IWarehouseManagerService warehouseManagerService, IWarehouseUserService warehouseUserService)
        {
            _warehouseManagerService = warehouseManagerService;
            _warehouseUserService = warehouseUserService;
        }

        [HttpPut]
        [Route("/products/quantity")]
        public IActionResult ChangeProductQuantity(UpdateProductQuantityRequestModel updateProductQuantityRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(updateProductQuantityRequestModel, AccessRights.Manager);
                if (error is not null) return BadRequest(error);
                error = _warehouseUserService.TryFindProduct(updateProductQuantityRequestModel);
                if (error != null)
                    return BadRequest(error);
                ActionWithProductSuccessModel response = _warehouseManagerService.ChangeProductQuantity(updateProductQuantityRequestModel);
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

        [HttpPut]
        [Route("/orders")]
        public IActionResult SendOrder(SendOrderRequestModel sendOrderRequest)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(sendOrderRequest, AccessRights.Manager);
                if (error is not null) return BadRequest(error);
                error = _warehouseUserService.TryFindOrder(sendOrderRequest);
                if (error != null)
                    return BadRequest(error);
                SendOrderSuccessModel response = _warehouseManagerService.SendOrder(sendOrderRequest);
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
