using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class UnauthorizedController : ControllerBase
    {
        private IWarehouseAuthService _warehouseAuthService;

        public UnauthorizedController(IWarehouseAuthService warehouseAuthService)
        {
            _warehouseAuthService = warehouseAuthService;
        }
        
        [HttpGet]
        [Route("/sign-up")]
        public IActionResult CheckLogin([FromQuery]TestLoginRequestModel checkLoginRequestModel)
        {
            try
            {
                TestLoginResponseModel response = _warehouseAuthService.TestLogin(checkLoginRequestModel);
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
        [Route("/sign-up")]
        public IActionResult CreateUser(SignInRequestModel createUserRequest)
        {
            try
            {
                SignInResponseModel response = _warehouseAuthService.TrySignIn(createUserRequest);
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
        [Route("/log-in")]
        public IActionResult LogIn([FromQuery]LogInRequestModel logInRequest)
        {
            try
            {
                TryLogInResponseModel response = _warehouseAuthService.TryLogIn(logInRequest);
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
