using Infrastructure.Interfaces;
using Models.Api.Common.Response;
using Models.Api.Common.Request;

namespace Infrastructure.Services
{
    public abstract class WarehouseUserService : IWarehouseUserService
    {
        protected readonly IProductsRepository _productsRepository;
        protected readonly IOrdersRepository _ordersRepository;

        public WarehouseUserService(IProductsRepository productsRepository, IOrdersRepository ordersRepository)
        {
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
        }

        public ErrorResponseModel TryFindProduct(ActionWithExistingProductRequestModel productRequest)
        {
            var product = _productsRepository.GetProduct(productRequest.ProductId);
            if (product is null) return new() { ErrorMessage = "product not found" };

            return null;
        }

        public ErrorResponseModel TryFindOrder(ActionWithExistingOrderRequestModel orderRequest)
        {
            var order = _ordersRepository.GetOrder(orderRequest.OrderId);
            if (order is null) return new() { ErrorMessage = "order not found" };

            return null;
        }

        public GetProductListSuccessModel GetProductsByCategory(GetProductListRequestModel productListRequest)
        {
            var products = _productsRepository.GetProductList(productListRequest.ProductCategory);
            List<ProductModel> productModels = new List<ProductModel>();
            foreach (var product in products)
            {
                productModels.Add(product);
            }

            return new()
            {
                Category = productListRequest.ProductCategory,
                ProductList = productModels
            };
        }
        
        public GetOrderListSuccessModel GetOrderList(GetOrderListRequestModel orderListRequest)
        {
            var orders = _ordersRepository.GetOrderList(orderListRequest.UserId, orderListRequest.ProductId);
            List<OrderModel> orderModels = new List<OrderModel>();
            foreach (var order in orders)
            {
                orderModels.Add(order);
            }

            return new()
            {
                OrderList = orderModels
            };
        }
    }
}
