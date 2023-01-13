using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Admin.Request;
using Models.Api.Req_Res.Common.Response;

namespace Infrastructure.Interfaces;

public interface IValidationService
{
    bool ValidateEmail(string? email, bool nullable);
    bool ValidatePhone(string? phone, bool nullable);
    bool ValidatePassword(string? password, bool nullable);
    bool ValidateName(string? name, bool nullable);

    ErrorResponseModel? ValidateUserModel(UserModel user, bool nullable = false);
    ErrorResponseModel? ValidateProductModel(AddProductRequestModel product);
}