using Models.Api.Common.Request;
using Models.Api.Common.Response;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseAuthService
    {
        TestLoginResponseModel TestLogin(TestLoginRequestModel login);
        SignInResponseModel TrySignIn(SignInRequestModel userData);
        TryLogInResponseModel TryLogIn(LogInRequestModel credentials);
    }
}
