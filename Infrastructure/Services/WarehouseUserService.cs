using Infrastructure.Interfaces;
using Models.Api.Common.Response;
using Models.Api.Common.Request;

namespace Infrastructure.Services
{
    public abstract class WarehouseUserService : IWarehouseUserService
    {
        protected readonly IProductsRepository _productsRepository;
        protected readonly ICustomersRepository _customersRepository;
        protected readonly IOrdersRepository _ordersRepository;

        public WarehouseUserService(IProductsRepository productsRepository, ICustomersRepository customersRepository, IOrdersRepository ordersRepository)
        {
            _productsRepository = productsRepository;
            _customersRepository = customersRepository;
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
    }
}
