using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Common.Request;
using Models.Api.Common.Response;
using Models.Api.Customer.Request;
using Models.DBModels;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomersController : CommonController
    {
        public CustomersController(IWarehouseCustomersService warehouseCustomersService) : base(warehouseCustomersService)
        {
            
        }
        
        [HttpGet]
        [Route("/myorders")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetOrderList(GetMyOrderListRequestModel getMyOrdersRequestModel)
        {
            try
            {
                (ErrorResponseModel error, User user) = _warehouseCustomersService.CheckRequest(getMyOrdersRequestModel, AccessRights.Customer);
                if (error is not null) return BadRequest(error);
                GetOrderListRequestModel getOrdersRequestModel = new()
                {
                    SessionId = getMyOrdersRequestModel.SessionId,
                    UserId = user.UserId
                };
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
        
        [HttpPost]
        [Route("/orders")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult CreateOrder(CreateMyOrderRequestModel createOrderRequest)
        {
            try
            {
                (ErrorResponseModel error, User user) = _warehouseCustomersService.CheckRequest(createOrderRequest, AccessRights.Customer);
                if (error is not null) return BadRequest(error);
                CreateOrderRequestModel request = new()
                {
                    SessionId = createOrderRequest.SessionId, 
                    Product = createOrderRequest.Product,
                    Quantity = createOrderRequest.Quantity,
                    User = user
                };
                ActionWithOrderSuccessModel response = _warehouseCustomersService.MakeOrder(request);
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
