using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Common.Response;
using Models.Api.Admin.Request;
using Models.Api.Admin.Response.Success;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AdminController : CommonController
    {
        private IWarehouseAdminService _warehouseAdminService;
        public AdminController(IWarehouseAdminService warehouseAdminService, IWarehouseCustomersService warehouseCustomersService) : base(warehouseCustomersService)
        {
            _warehouseAdminService = warehouseAdminService;
        }

        [HttpPost]
        [Route("/add/product")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult AddProduct(AddProductRequestModel addProductRequestModel)
        {
            try
            {
                var product = addProductRequestModel.ConvertToProduct();
                ErrorResponseModel error = _warehouseAdminService.ValidateProductModel(addProductRequestModel);
                if (error != null)
                    return BadRequest(error);
                AddProductSuccessModel response = _warehouseAdminService.AddProduct(product);
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
        [Route("/delete/product")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult DeleteProduct(DeleteProductRequestModel deleteProductRequestModel)
        {
            try
            {
                ErrorResponseModel error = _warehouseAdminService.TryFindProduct(deleteProductRequestModel);
                if (error != null)
                    return BadRequest(error);
                DeleteProductSuccessModel response = _warehouseAdminService.DeleteProduct(deleteProductRequestModel);
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
        [Route("/update/product/price")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult UpdateProductPrice(UpdateProductPriceRequestModel updateProductPriceRequestModel)
        {
            try
            {
                ErrorResponseModel error = _warehouseAdminService.TryFindProduct(updateProductPriceRequestModel);
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
        
        [HttpPost]
        [Route("/update/order/reject")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult RejectOrder(RejectOrderRequestModel rejectOrderRequest)
        {
            try
            {
                ErrorResponseModel error = _warehouseAdminService.TryFindOrder(rejectOrderRequest);
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
