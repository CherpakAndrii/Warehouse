using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Common.Response;
using Models.Api.Manager.Request;
using Models.Api.Manager.Response.Success;
using Models.DBModels;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ManagerController : WareHouseWorkerController
    {
        private readonly IWarehouseManagerService _warehouseManagerService;
        public ManagerController(IWarehouseManagerService warehouseManagerService, IWarehouseCustomersService warehouseCustomersService): base (warehouseCustomersService)
        {
            _warehouseManagerService = warehouseManagerService;
        }

        [HttpPut]
        [Route("/update/product/quantity")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult ChangeProductQuantity(UpdateProductQuantityRequestModel updateProductQuantityRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseCustomersService.CheckRequest(updateProductQuantityRequestModel, AccessRights.Manager);
                if (error is not null) return BadRequest(error);
                error = _warehouseManagerService.TryFindProduct(updateProductQuantityRequestModel);
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
        [Route("/update/order/send")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult SendOrder(SendOrderRequestModel sendOrderRequest)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseCustomersService.CheckRequest(sendOrderRequest, AccessRights.Manager);
                if (error is not null) return BadRequest(error);
                error = _warehouseManagerService.TryFindOrder(sendOrderRequest);
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
