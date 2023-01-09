using Infrastructure.Interfaces;
using Models.Api.Manager.Request;
using Models.Api.Manager.Response.Success;
using Models.DBModels.Enums;

namespace Infrastructure.Services;

public class WarehouseManagerService : WarehouseUserService, IWarehouseManagerService
{

    public WarehouseManagerService(IProductsRepository productsRepository, IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository) : base(productsRepository, ordersRepository, sessionsRepository) { }

    public UpdateProductQuantitySuccessModel ChangeProductQuantity(UpdateProductQuantityRequestModel product)
    {
        var changedQuantityProduct = _productsRepository.GetProduct(product.ProductId);
        if (changedQuantityProduct.Quantity < product.ProductQuantityDifference) throw new ArgumentException("Impossible to remove more products than are available");
        changedQuantityProduct.Quantity -= product.ProductQuantityDifference;
        _productsRepository.UpdateProduct(changedQuantityProduct);
        return new()
        {
            Product = changedQuantityProduct
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