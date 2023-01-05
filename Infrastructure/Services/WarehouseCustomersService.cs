using Infrastructure.Interfaces;
using Models.Api.Common.Request;
using Models.Api.Common.Response;
using Models.DBModels;

namespace Infrastructure.Services
{
    public class WarehouseCustomersService : WarehouseUserService, IWarehouseCustomersService
    {
        public WarehouseCustomersService(IProductsRepository productsRepository, IOrdersRepository ordersRepository) : base(productsRepository, ordersRepository)
        {
        }
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
    }
}
