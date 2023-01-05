﻿using Infrastructure.Interfaces;
using Models.Api.Manager.Request;
using Models.Api.Manager.Response.Success;
using Models.DBModels.Enums;

namespace Infrastructure.Services;

public class WarehouseManagerService : WarehouseUserService, IWarehouseManagerService
{

    public WarehouseManagerService(IProductsRepository productsRepository, IOrdersRepository ordersRepository) : base(productsRepository, ordersRepository) { }

    public UpdateProductQuantitySuccessModel AddProductQuantity(IncreaseProductQuantityRequestModel product)
    {
        var increasedQuantityProduct = _productsRepository.GetProduct(product.ProductId);
        increasedQuantityProduct.Quantity += product.ProductQuantityToAdd;
        _productsRepository.UpdateProduct(increasedQuantityProduct);
        return new()
        {
            ProductName = increasedQuantityProduct.Name,
            ProductId = increasedQuantityProduct.ProductId,
            ProductQuantity = increasedQuantityProduct.Quantity, 
            ProductPrice = increasedQuantityProduct.Price,
            ProductCategory = increasedQuantityProduct.Category
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
            ProductName = decreasedQuantityProduct.Name,
            ProductId = decreasedQuantityProduct.ProductId,
            ProductQuantity = decreasedQuantityProduct.Quantity,
            ProductPrice = decreasedQuantityProduct.Price,
            ProductCategory = decreasedQuantityProduct.Category
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
            OrderId = sentOrder.OrderId,
            Status = sentOrder.Status,
            ProductName = sentOrder.Product.Name,
            Quantity = sentOrder.Quantity,
            OrderPrice = sentOrder.OrderPrice,
            CustomerName = sentOrder.User.Name
        };
    }

    public string GetAllOrders()
    {
        throw new NotImplementedException();
    }
}