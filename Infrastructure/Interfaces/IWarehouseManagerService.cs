using Models.Api.Manager.Request;
using Models.Api.Manager.Response.Success;

namespace Infrastructure.Interfaces;

public interface IWarehouseManagerService : IWarehouseUserService
{
    UpdateProductQuantitySuccessModel ChangeProductQuantity(UpdateProductQuantityRequestModel product);
    SendOrderSuccessModel SendOrder(SendOrderRequestModel sendOrderRequest);
}