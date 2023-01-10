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
        private readonly IWarehouseUserService _warehouseUserService;

        protected CommonController(IWarehouseUserService warehouseUserService)
        {
            _warehouseUserService = warehouseUserService;
        }

        [HttpGet]
        [Route("/products")]
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetProductList(GetProductListRequestModel getProductsRequestModel)
        {
            try
            {
                (ErrorResponseModel error, _) = _warehouseUserService.CheckRequest(getProductsRequestModel, AccessRights.Any);
                if (error is not null) return BadRequest(error);
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
        //[Authorize(Policy = "Authorize")]
        public IActionResult GetMyProfile(GetMyProfileRequestModel getMyProfileRequest)
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
        //[Authorize(Policy = "Authorize")]
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
