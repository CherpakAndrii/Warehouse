using Infrastructure.Interfaces;
using Models.Api.Common.Response;
using Models.Api.Customer.Request;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services
{
    public class WarehouseCustomersService : WarehouseUserService, IWarehouseCustomersService
    {
        public WarehouseCustomersService(IProductsRepository productsRepository, IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository) : base(productsRepository, ordersRepository, sessionsRepository) { }

        public ActionWithOrderSuccessModel MakeOrder(CreateOrderRequestModel createRequest)
        {
            Order newOrder = new Order()
            {
                Status = OrderStatus.Created,
                Product = createRequest.Product,
                Quantity = createRequest.Quantity,
                OrderPrice = createRequest.Product.Price * createRequest.Quantity,
                User = createRequest.User
            };
            _ordersRepository.CreateOrder(newOrder);
            var addedOrder = _ordersRepository.GetJustCreatedOrder(newOrder); // to get orderId
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
