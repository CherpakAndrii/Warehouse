using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseUserService
    {
        ErrorResponseModel TryFindProduct(ActionWithExistingProductRequestModel product);
        ErrorResponseModel TryFindOrder(ActionWithExistingOrderRequestModel orderRequest);
        GetProductListSuccessModel GetProductsByCategory(GetProductListRequestModel productListRequest);
        GetOrderListSuccessModel GetOrderList(GetOrderListRequestModel orderListRequest);
        (ErrorResponseModel, User) CheckRequest(CommonUserRequestModel request, AccessRights neededRights);
        (ErrorResponseModel, User) AdvancedCheckRequest(AdditionalSecurityRequestModel request, AccessRights neededRights);
        GetMyProfileResponseModel GetMyProfileDetails(GetMyProfileRequestModel getMyProfileRequest);
        UpdateMyProfileResponseModel UpdateMyProfile(UpdateMyProfileRequestModel updateProfileRequest);
    }
}
