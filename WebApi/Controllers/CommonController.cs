using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Common.Response;
using Models.Api.Common.Request;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private IWarehouseCustomersService _warehouseCustomersService;
        public CommonController(IWarehouseCustomersService warehouseCustomersService)
        {
            _warehouseCustomersService = warehouseCustomersService;
        }

        [HttpPost]
        [Route("/products")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetProductList(GetProductListRequestModel getProductsRequestModel)
        {
            try
            {
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
