using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseAuthService
    {
        TestLoginResponseModel TestLogin(TestLoginRequestModel login);
        SignInResponseModel TrySignIn(SignInRequestModel userData);
        TryLogInResponseModel TryLogIn(LogInRequestModel credentials);
    }
}
