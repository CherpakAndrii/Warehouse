using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels.Enums;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IWarehouseUserService _warehouseUserService;

        public CommonController(IWarehouseUserService warehouseUserService)
        {
            _warehouseUserService = warehouseUserService;
        }

        [HttpGet]
        [Route("/products")]
        public IActionResult GetProductList([FromQuery] GetProductListRequestModel getProductsRequestModel)
        {
            try
            {
                GetProductListSuccessModel response = _warehouseUserService.GetProductsByCategory(getProductsRequestModel);
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
        
        [HttpGet]
        [Route("/my-profile")]
        public IActionResult GetMyProfile([FromQuery] GetMyProfileRequestModel getMyProfileRequest)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(getMyProfileRequest, AccessRights.Any);
                if (error is not null) return BadRequest(error);
                GetMyProfileResponseModel response = _warehouseUserService.GetMyProfileDetails(getMyProfileRequest);
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
        [Route("/my-profile")]
        public IActionResult UpdateMyProfile(UpdateMyProfileRequestModel updateMyProfileRequest)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.AdvancedCheckRequest(updateMyProfileRequest, AccessRights.Any);
                if (error is not null) return BadRequest(error);
                UpdateMyProfileResponseModel response = _warehouseUserService.UpdateMyProfile(updateMyProfileRequest);
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
