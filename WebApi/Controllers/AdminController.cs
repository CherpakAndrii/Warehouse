using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Api;
using Models.Api.Admin.Request;
using Models.Api.Admin.Response.Success;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IWarehouseAdminService _warehouseAdminService;
        private IWarehouseCustomersService _warehouseCustomersService;
        public AdminController(IWarehouseAdminService warehouseAdminService, IWarehouseCustomersService warehouseCustomersService)
        {
            _warehouseAdminService = warehouseAdminService;
            _warehouseCustomersService = warehouseCustomersService;
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
                return Ok(addProductRequestModel);
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
                DeleteProductSuccessModel response = _warehouseAdminService.DeleteProduct(deleteProductRequestModel.ProductId);
                if (response == null)
                    return StatusCode(500);
                return Ok(deleteProductRequestModel);
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
