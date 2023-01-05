using Models.Api.Admin.Request;
using Models.Api.Admin.Response.Success;
using Models.Api.Common.Request;
using Models.Api.Common.Response;
using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseUserService
    {
        ErrorResponseModel TryFindProduct(ActionWithExistingProductRequestModel product);
        ErrorResponseModel TryFindOrder(ActionWithExistingOrderRequestModel orderRequest);
    }
}
