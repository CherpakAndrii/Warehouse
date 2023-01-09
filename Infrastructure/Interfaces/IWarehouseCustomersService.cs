using Models.Api.Common.Response;
using Models.Api.Customer.Request;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseCustomersService : IWarehouseUserService
    {
        ActionWithOrderSuccessModel MakeOrder(CreateOrderRequestModel createRequest);
        ActionWithOrderSuccessModel RemoveOrder(RemoveOrderRequestModel removeOrderRequest);
        
        string GetProductInfo();
    }
}
