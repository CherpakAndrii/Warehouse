using Infrastructure.Interfaces;
using Models.Api.Manager.Request;
using Models.Api.Manager.Response.Success;
using Models.DBModels.Enums;

namespace Infrastructure.Services;

public class WarehouseManagerService : WarehouseUserService, IWarehouseManagerService
{

    public WarehouseManagerService(IProductsRepository productsRepository, IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository) : base(productsRepository, ordersRepository, sessionsRepository) { }

    public UpdateProductQuantitySuccessModel AddProductQuantity(IncreaseProductQuantityRequestModel product)
    {
        var increasedQuantityProduct = _productsRepository.GetProduct(product.ProductId);
        increasedQuantityProduct.Quantity += product.ProductQuantityToAdd;
        _productsRepository.UpdateProduct(increasedQuantityProduct);
        return new()
        {
            Product = increasedQuantityProduct
            
        };
    }

    // Sometimes something can happen to products (expiry, theft, fire damage). Therefore, the administrator should be able to reduce the number of products
    public UpdateProductQuantitySuccessModel DecreaseProductQuantity(DecreaseProductQuantityRequestModel product)
    {
        var decreasedQuantityProduct = _productsRepository.GetProduct(product.ProductId);
        if (decreasedQuantityProduct.Quantity < product.ProductQuantityToDecrease) throw new ArgumentException("Impossible to remove more products than are available");
        decreasedQuantityProduct.Quantity -= product.ProductQuantityToDecrease;
        _productsRepository.UpdateProduct(decreasedQuantityProduct);
        return new()
        {
            Product = decreasedQuantityProduct
        };
    }

    public SendOrderSuccessModel SendOrder(SendOrderRequestModel orderRequest)
    {
        var sentOrder = _ordersRepository.GetOrder(orderRequest.OrderId);
        if (sentOrder.Status == OrderStatus.Sent) throw new ArgumentException("This order is already sent");
        if (sentOrder.Status == OrderStatus.Rejected) throw new ArgumentException("This order is rejected");
        sentOrder.Status = OrderStatus.Sent;
        _ordersRepository.UpdateOrder(sentOrder);
        return new()
        {
            Order = sentOrder
        };
    }

    public string GetAllOrders()
    {
        throw new NotImplementedException();
    }
}