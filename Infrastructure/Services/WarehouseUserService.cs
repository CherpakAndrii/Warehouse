using Infrastructure.Interfaces;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services
{
    public abstract class WarehouseUserService : IWarehouseUserService
    {
        protected readonly IProductsRepository ProductsRepository;
        protected readonly IOrdersRepository OrdersRepository;
        private readonly ISessionsRepository _sessionsRepository;

        public WarehouseUserService(IProductsRepository productsRepository, IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository)
        {
            ProductsRepository = productsRepository;
            OrdersRepository = ordersRepository;
            _sessionsRepository = sessionsRepository;
        }

        public ErrorResponseModel TryFindProduct(ActionWithExistingProductRequestModel productRequest)
        {
            var product = ProductsRepository.GetProduct(productRequest.ProductId);
            if (product is null) return new() { ErrorMessage = "product not found" };

            return null;
        }

        public ErrorResponseModel TryFindOrder(ActionWithExistingOrderRequestModel orderRequest)
        {
            var order = OrdersRepository.GetOrder(orderRequest.OrderId);
            if (order is null) return new() { ErrorMessage = "order not found" };

            return null;
        }

        public GetProductListSuccessModel GetProductsByCategory(GetProductListRequestModel productListRequest)
        {
            var products = ProductsRepository.GetProductList(productListRequest.ProductCategory);
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
            var orders = OrdersRepository.GetOrderList(orderListRequest.UserId, orderListRequest.ProductId);
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
            if (request.Login != requestUser.Login)
            {
                _sessionsRepository.CloseSessionById(request.SessionId); // someone is trying to use another user`s session!  
                return (new() { ErrorMessage = "409 Conflict" }, null);
            }
            var r = requestUser.Role;
            if (r == UserRole.Admin && (int)neededRights % 2 == 0 ||
                r == UserRole.Manager && (int)neededRights % 3 != 0 ||
                r == UserRole.Customer && (int)neededRights > 2)
                return (null, requestUser);
            return (new() { ErrorMessage = "403 Forbidden" }, null);
        }

        public string GetMyProfileDetails()
        {
            throw new NotImplementedException();
        }

        public string UpdateMyProfile()
        {
            throw new NotImplementedException();
        }
    }
}
