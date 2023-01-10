using Models.Api.Common.Request;
using Models.Api.Common.Response;
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
        string GetMyProfileDetails();
        string UpdateMyProfile();
    }
}
