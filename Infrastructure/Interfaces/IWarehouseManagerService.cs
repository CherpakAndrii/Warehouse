using Models.Api.Req_Res.Manager.Request;
using Models.Api.Req_Res.Manager.Response;

namespace Infrastructure.Interfaces;

public interface IWarehouseManagerService : IWarehouseUserService
{
    UpdateProductQuantitySuccessModel ChangeProductQuantity(UpdateProductQuantityRequestModel product);
    SendOrderSuccessModel SendOrder(SendOrderRequestModel sendOrderRequest);
}