using Infrastructure.Interfaces;
using Models.Api.Common.Response;
using Models.Api.Customer.Request;

namespace Infrastructure.Services
{
    public class WarehouseCustomersService : WarehouseUserService, IWarehouseCustomersService
    {
        public WarehouseCustomersService(IProductsRepository productsRepository, IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository) : base(productsRepository, ordersRepository, sessionsRepository) { }

        public ActionWithOrderSuccessModel MakeOrder(CreateOrderRequestModel createRequest)
        {
            _ordersRepository.CreateOrder(createRequest.Order);
            var addedOrder = _ordersRepository.GetOrder();
            return new()
            {
                Order = addedOrder
            };
        }

        public ActionWithOrderSuccessModel RemoveOrder(RemoveOrderRequestModel removeOrderRequest)
        {
            var deletedOrder = _ordersRepository.GetOrder(removeOrderRequest.OrderId);
            _ordersRepository.DeleteOrder(deletedOrder);
            return new()
            {
                Order = deletedOrder
            };
        }

        public string GetProductInfo()
        {
            throw new NotImplementedException();
        }
    }
}
