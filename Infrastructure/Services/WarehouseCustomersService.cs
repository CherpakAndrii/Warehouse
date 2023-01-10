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
            Product orderedProduct = _productsRepository.GetProduct(createRequest.Product.ProductId);
            Order newOrder = new Order
            {
                Status = OrderStatus.Created,
                Product = orderedProduct,
                Quantity = createRequest.Quantity,
                OrderPrice = createRequest.Product.Price * createRequest.Quantity,
                User = createRequest.User
            };
            _ordersRepository.CreateOrder(newOrder);
            orderedProduct.AvailableAmount -= (int)createRequest.Quantity;
            _productsRepository.UpdateProduct(orderedProduct);
            var addedOrder = _ordersRepository.GetJustCreatedOrder(newOrder); // to get orderId
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
            if (deletedOrder.User.UserId != removeOrderRequest.UserId)
                return new() { Success = false, Message = "can't remove another user's order", Order = deletedOrder };
            if (deletedOrder.Status == OrderStatus.Sent)
                return new() { Success = false, Message = "can't remove already sent order", Order = deletedOrder };
            if (deletedOrder.Status == OrderStatus.Rejected)
                return new() { Success = false, Message = "this order is already rejected", Order = deletedOrder };
            Product orderedProduct = _productsRepository.GetProduct(deletedOrder.Product.ProductId!.Value);
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
