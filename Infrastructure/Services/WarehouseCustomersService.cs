using Infrastructure.Interfaces;
using Models.Api.Common.Request;
using Models.Api.Common.Response;
using Models.DBModels;

namespace Infrastructure.Services
{
    public class WarehouseCustomersService : IWarehouseUserService, IWarehouseCustomersService
    {
        public string GetMyOrders()
        {
            throw new NotImplementedException();
        }

        public string GetProductInfo()
        {
            throw new NotImplementedException();
        }

        public string GetProducts(string category = null)
        {
            throw new NotImplementedException();
        }

        public string MakeOrder(Product product)
        {
            throw new NotImplementedException();
        }

        public ErrorResponseModel TryFindProduct(ActionWithExistingProductRequestModel product)
        {
            throw new NotImplementedException();
        }

        public ErrorResponseModel TryFindOrder(ActionWithExistingOrderRequestModel orderRequest)
        {
            throw new NotImplementedException();
        }
    }
}
