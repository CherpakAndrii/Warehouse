using Infrastructure.Interfaces;
using Models.Api.Req_Res.Manager.Request;
using Models.Api.Req_Res.Manager.Response;
using Models.DBModels.Enums;

namespace Infrastructure.Services;

public class WarehouseManagerService : IWarehouseManagerService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IOrdersRepository _ordersRepository;

    public WarehouseManagerService(IProductsRepository productsRepository, IOrdersRepository ordersRepository)
    {
        _productsRepository = productsRepository;
        _ordersRepository = ordersRepository;
    }

    public UpdateProductQuantitySuccessModel ChangeProductQuantity(UpdateProductQuantityRequestModel productChanges)
    {
        var changedQuantityProduct = _productsRepository.GetProduct(productChanges.ProductId);
        if (changedQuantityProduct.AvailableAmount < productChanges.ProductQuantityDifference) throw new ArgumentException("Impossible to remove more products than are available");
        changedQuantityProduct.Quantity -= productChanges.ProductQuantityDifference;
        changedQuantityProduct.AvailableAmount -= (int)productChanges.ProductQuantityDifference;
        _productsRepository.UpdateProduct(changedQuantityProduct);
        return new()
        {
            Product = changedQuantityProduct
        };
    }

    public SendOrderSuccessModel SendOrder(SendOrderRequestModel orderRequest)
    {
        var sentOrder = _ordersRepository.GetOrder(orderRequest.OrderId);
        if (sentOrder.Status == OrderStatus.Sent) return new() { Success = false, Order = sentOrder, Message =  "This order is already sent"};
        if (sentOrder.Status == OrderStatus.Rejected) return new() { Success = false, Order = sentOrder, Message =  "This order is rejected"};
        var orderedProduct = sentOrder.Product;
        if (sentOrder.Quantity > orderedProduct.Quantity) return new() { Success = false, Order = sentOrder, Message =  "Not enough products"};
        sentOrder.Status = OrderStatus.Sent;
        orderedProduct.Quantity -= sentOrder.Quantity;
        _ordersRepository.UpdateOrder(sentOrder);
        _productsRepository.UpdateProduct(orderedProduct);
        return new()
        {
            Success = true,
            Order = sentOrder,
            Message = "Successfully sent"
        };
    }
}