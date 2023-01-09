using Infrastructure.Interfaces;
using Models.Api.ApiEntityModels;
using Models.Api.Common.Response;
using Models.Api.Common.Request;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services
{
    public abstract class WarehouseUserService : IWarehouseUserService
    {
        protected readonly IProductsRepository _productsRepository;
        protected readonly IOrdersRepository _ordersRepository;
        protected readonly ISessionsRepository _sessionsRepository;

        public WarehouseUserService(IProductsRepository productsRepository, IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository)
        {
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            _sessionsRepository = sessionsRepository;
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

        public (ErrorResponseModel, User) CheckRequest(CommonUserRequestModel request, AccessRights neededRights)
        {
            User requestUser = _sessionsRepository.GetUserBySessionId(request.SessionId);
            if (requestUser is null) return (new() { ErrorMessage = "401 Unauthorized" }, null);
            var r = requestUser.Role;
            if (r == UserRole.Admin && (int)neededRights % 2 == 0 ||
                r == UserRole.Manager && (int)neededRights % 3 != 0 ||
                r == UserRole.Customer && (int)neededRights > 2)
                return (null, requestUser);
            return (new() { ErrorMessage = "403 Forbidden" }, null);
        }
    }
}
