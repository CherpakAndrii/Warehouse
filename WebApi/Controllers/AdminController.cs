using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Admin.Request;
using Models.Api.Req_Res.Admin.Response;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IWarehouseAdminService _warehouseAdminService;
        private readonly IWarehouseUserService _warehouseUserService;
        public AdminController(IWarehouseAdminService warehouseAdminService, IWarehouseUserService warehouseUserService, IWarehouseCustomersService warehouseCustomersService)
        {
            _warehouseAdminService = warehouseAdminService;
            _warehouseUserService = warehouseUserService;
        }

        [HttpPost]
        [Route("/products")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult AddProduct(AddProductRequestModel addProductRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(addProductRequestModel, AccessRights.Admin);
                if (error is not null) return BadRequest(error);
                error = _warehouseAdminService.ValidateProductModel(addProductRequestModel);
                if (error != null)
                    return BadRequest(error);
                AddProductSuccessModel response = _warehouseAdminService.AddProduct(addProductRequestModel);
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
        [Route("/products")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult DeleteProduct(DeleteProductRequestModel deleteProductRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(deleteProductRequestModel, AccessRights.Admin);
                if (error is not null) return BadRequest(error);
                error = _warehouseUserService.TryFindProduct(deleteProductRequestModel);
                if (error != null)
                    return BadRequest(error);
                List<OrderModel> relatedOrders = _warehouseUserService.GetOrderList(new() { ProductId = deleteProductRequestModel.ProductId }).OrderList;
                int rejectedOrderCtr = 0;
                foreach (var order in relatedOrders)
                {
                    if (_warehouseAdminService.RejectOrder(new RejectOrderRequestModel(){OrderId = order.OrderId}).Success) rejectedOrderCtr++;
                }
                
                DeleteProductSuccessModel delResponse = _warehouseAdminService.DeleteProduct(deleteProductRequestModel);
                if (delResponse == null)
                    return StatusCode(500);
                
                DeleteProductSuccessModel rejResponse = new DeleteProductSuccessModel()
                {
                    OrdersRejected = rejectedOrderCtr,
                    Product = delResponse.Product
                };
                
                return Ok(rejResponse);
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
        [Route("/product")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult UpdateProductPrice(UpdateProductPriceRequestModel updateProductPriceRequestModel)
        {
            try
            {
                
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(updateProductPriceRequestModel, AccessRights.Admin);
                if (error is not null) return BadRequest(error);
                error = _warehouseUserService.TryFindProduct(updateProductPriceRequestModel);
                if (error != null)
                    return BadRequest(error);
                UpdateProductPriceSuccessModel response = _warehouseAdminService.UpdateProductPrice(updateProductPriceRequestModel);
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
        [Route("/orders")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult RejectOrder(RejectOrderRequestModel rejectOrderRequest)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(rejectOrderRequest, AccessRights.Admin);
                if (error is not null) return BadRequest(error);
                error = _warehouseUserService.TryFindOrder(rejectOrderRequest);
                if (error != null)
                    return BadRequest(error);
                RejectOrderSuccessModel response = _warehouseAdminService.RejectOrder(rejectOrderRequest);
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
