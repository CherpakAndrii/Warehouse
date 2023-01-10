using Models.Api.Req_Res.Customer.Request;
using Models.Api.Req_Res.Customer.Response;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseCustomersService : IWarehouseUserService
    {
        CreateOrderResponseModel MakeOrder(CreateOrderRequestModel createRequest);
        RemoveOrderResponseModel RemoveOrder(RemoveOrderRequestModel removeOrderRequest);
    }
}
