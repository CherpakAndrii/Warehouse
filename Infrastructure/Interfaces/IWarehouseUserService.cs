using Models.Api.Common.Request;
using Models.Api.Common.Response;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseUserService
    {
        ErrorResponseModel TryFindProduct(ActionWithExistingProductRequestModel product);
        ErrorResponseModel TryFindOrder(ActionWithExistingOrderRequestModel orderRequest);
        GetProductListSuccessModel GetProductsByCategory(GetProductListRequestModel productListRequest);
        GetOrderListSuccessModel GetOrderList(GetOrderListRequestModel orderListRequest);
    }
}
