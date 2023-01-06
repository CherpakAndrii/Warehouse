using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Common.Response;
using Models.Api.Manager.Request;
using Models.Api.Manager.Response.Success;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ManagerController : WareHouseWorkerController
    {
        protected IWarehouseManagerService _warehouseManagerService;
        public ManagerController(IWarehouseManagerService warehouseManagerService, IWarehouseCustomersService warehouseCustomersService): base (warehouseCustomersService)
        {
            _warehouseManagerService = warehouseManagerService;
        }

        [HttpPost]
        [Route("/update/product/quantity/increase")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult IncreaseProductQuantity(IncreaseProductQuantityRequestModel increaseProductQuantityRequestModel)
        {
            try
            {
                ErrorResponseModel error = _warehouseManagerService.TryFindProduct(increaseProductQuantityRequestModel);
                if (error != null)
                    return BadRequest(error);
                ActionWithProductSuccessModel response = _warehouseManagerService.AddProductQuantity(increaseProductQuantityRequestModel);
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
        
        [HttpPost]
        [Route("/update/product/quantity/decrease")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult DecreaseProductQuantity(DecreaseProductQuantityRequestModel decreaseProductQuantityRequestModel)
        {
            try
            {
                ErrorResponseModel error = _warehouseManagerService.TryFindProduct(decreaseProductQuantityRequestModel);
                if (error != null)
                    return BadRequest(error);
                ActionWithProductSuccessModel response = _warehouseManagerService.DecreaseProductQuantity(decreaseProductQuantityRequestModel);
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
        
        [HttpPost]
        [Route("/update/order/send")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult SendOrder(SendOrderRequestModel sendOrderRequest)
        {
            try
            {
                ErrorResponseModel error = _warehouseManagerService.TryFindOrder(sendOrderRequest);
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
