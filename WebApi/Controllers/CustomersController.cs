﻿using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.Api.Req_Res.Customer.Request;
using Models.Api.Req_Res.Customer.Response;
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
        [Route("/my-orders")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetOrderList(GetMyOrderListRequestModel getMyOrdersRequestModel)
        {
            try
            {
                (ErrorResponseModel error, User user) = WarehouseCustomersService.CheckRequest(getMyOrdersRequestModel, AccessRights.Customer);
                if (error is not null) return BadRequest(error);
                GetOrderListRequestModel getOrdersRequestModel = new()
                {
                    SessionId = getMyOrdersRequestModel.SessionId,
                    UserId = user.UserId
                };
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
        
        [HttpPost]
        [Route("/my-orders")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult CreateOrder(CreateMyOrderRequestModel createOrderRequest)
        {
            try
            {
                (ErrorResponseModel error, User user) = WarehouseCustomersService.CheckRequest(createOrderRequest, AccessRights.Customer);
                if (error is not null) return BadRequest(error);
                CreateOrderRequestModel request = new()
                {
                    SessionId = createOrderRequest.SessionId, 
                    Product = createOrderRequest.Product,
                    Quantity = createOrderRequest.Quantity,
                    User = user
                };
                CreateOrderResponseModel response = WarehouseCustomersService.MakeOrder(request);
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
        
        [HttpDelete]
        [Route("/my-orders")]
        public IActionResult RemoveOrder(RemoveMyOrderRequestModel removeOrderRequest)
        {
            try
            {
                (ErrorResponseModel error, User user) = WarehouseCustomersService.CheckRequest(removeOrderRequest, AccessRights.Customer);
                if (error is not null) return BadRequest(error);
                RemoveOrderRequestModel request = new()
                {
                    SessionId = removeOrderRequest.SessionId,
                    Login = removeOrderRequest.Login,
                    OrderId = removeOrderRequest.OrderId,
                    UserId = user.UserId.Value
                };
                RemoveOrderResponseModel response = WarehouseCustomersService.RemoveOrder(request);
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
