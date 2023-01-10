using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public abstract class CommonController : ControllerBase
    {
        protected readonly IWarehouseCustomersService WarehouseCustomersService;

        protected CommonController(IWarehouseCustomersService warehouseCustomersService)
        {
            WarehouseCustomersService = warehouseCustomersService;
        }

        [HttpGet]
        [Route("/products")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetProductList(GetProductListRequestModel getProductsRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = WarehouseCustomersService.CheckRequest(getProductsRequestModel, AccessRights.Any);
                if (error is not null) return BadRequest(error);
                GetProductListSuccessModel response = WarehouseCustomersService.GetProductsByCategory(getProductsRequestModel);
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
