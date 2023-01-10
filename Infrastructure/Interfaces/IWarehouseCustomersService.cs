using Models.Api.Common.Response;
using Models.Api.Customer.Request;
using Models.Api.Customer.Response;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseCustomersService : IWarehouseUserService
    {
        CreateOrderResponseModel MakeOrder(CreateOrderRequestModel createRequest);
        RemoveOrderResponseModel RemoveOrder(RemoveOrderRequestModel removeOrderRequest);
    }
}
