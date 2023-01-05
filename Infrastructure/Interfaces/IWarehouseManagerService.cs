using Models.Api.Admin.Response.Success;
using Models.Api.Common.Request;
using Models.Api.Common.Response;
using Models.Api.Manager.Request;
using Models.Api.Manager.Response.Success;

namespace Infrastructure.Interfaces;

public interface IWarehouseManagerService : IWarehouseUserService
{
    UpdateProductQuantitySuccessModel AddProductQuantity(IncreaseProductQuantityRequestModel product);
    UpdateProductQuantitySuccessModel DecreaseProductQuantity(DecreaseProductQuantityRequestModel product);
    SendOrderSuccessModel SendOrder(SendOrderRequestModel sendOrderRequest);

    string GetAllOrders();
}