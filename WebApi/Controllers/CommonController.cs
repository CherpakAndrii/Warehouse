using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Common.Response;
using Models.Api.Common.Request;
using Models.DBModels;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public abstract class CommonController : ControllerBase
    {
        protected IWarehouseCustomersService _warehouseCustomersService;
        public CommonController(IWarehouseCustomersService warehouseCustomersService)
        {
            _warehouseCustomersService = warehouseCustomersService;
        }

        [HttpGet]
        [Route("/products")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetProductList(GetProductListRequestModel getProductsRequestModel)
        {
            try
            {
                (ErrorResponseModel error, User user) = _warehouseCustomersService.CheckRequest(getProductsRequestModel, AccessRights.Any);
                if (error is not null) return BadRequest(error);
                GetProductListSuccessModel response = _warehouseCustomersService.GetProductsByCategory(getProductsRequestModel);
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
