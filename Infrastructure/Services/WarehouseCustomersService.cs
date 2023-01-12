using Infrastructure.Interfaces;
using Models.Api.Req_Res.Customer.Request;
using Models.Api.Req_Res.Customer.Response;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services
{
    public class WarehouseCustomersService : IWarehouseCustomersService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersRepository _ordersRepository;

        public WarehouseCustomersService(IProductsRepository productsRepository, IOrdersRepository ordersRepository)
        {
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
        }

        public CreateOrderResponseModel MakeOrder(CreateOrderRequestModel createRequest)
        {
            Product orderedProduct = _productsRepository.GetProduct(createRequest.ProductId);
            Order newOrder = new Order
            {
                Status = OrderStatus.Created,
                ProductId = orderedProduct.ProductId!.Value,
                Quantity = createRequest.Quantity,
                OrderPrice = orderedProduct.Price * createRequest.Quantity,
                UserId = createRequest.UserId
            };
            var oldOrdersList = _ordersRepository.GetOrderList(createRequest.UserId, createRequest.ProductId).ToList();
            _ordersRepository.CreateOrder(newOrder);
            orderedProduct.AvailableAmount -= (int)createRequest.Quantity;
            _productsRepository.UpdateProduct(orderedProduct);
            var newOrdersList = _ordersRepository.GetOrderList(createRequest.UserId, createRequest.ProductId).ToList();

            var addedOrder = newOrdersList.Except(oldOrdersList).FirstOrDefault();
            return new() 
            {
                Order = addedOrder,
                Success = true,
                Message = "Order created successfully"
            };
        }

        public RemoveOrderResponseModel RemoveOrder(RemoveOrderRequestModel removeOrderRequest)
        {
            var deletedOrder = _ordersRepository.GetOrder(removeOrderRequest.OrderId);
            if (deletedOrder is null)
                return new() { Success = false, Message = $"order {removeOrderRequest.OrderId} not found" };
            if (deletedOrder.UserId != removeOrderRequest.UserId)
                return new() { Success = false, Message = "can't remove another user's order", Order = deletedOrder };
            if (deletedOrder.Status == OrderStatus.Sent)
                return new() { Success = false, Message = "can't remove already sent order", Order = deletedOrder };
            if (deletedOrder.Status == OrderStatus.Rejected)
                return new() { Success = false, Message = "this order is already rejected", Order = deletedOrder };
            Product orderedProduct = _productsRepository.GetProduct(deletedOrder.ProductId);
            orderedProduct.AvailableAmount += (int)deletedOrder.Quantity;
            _ordersRepository.DeleteOrder(deletedOrder);
            _productsRepository.UpdateProduct(orderedProduct);
            return new()
            {
                Order = deletedOrder,
                Success = true,
                Message = "Order deleted successfully"
            };
        }
    }
}
