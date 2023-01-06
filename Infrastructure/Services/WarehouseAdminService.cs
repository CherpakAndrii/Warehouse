using Infrastructure.Interfaces;
using Models.Api.Common.Response;
using Models.Api.Admin.Request;
using Models.Api.Admin.Response.Success;
using Models.Api.Common.Request;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services
{
    public class WarehouseAdminService : WarehouseUserService, IWarehouseAdminService
    {
        public WarehouseAdminService(IProductsRepository productsRepository, IUsersRepository usersRepository, IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository) : base(productsRepository, ordersRepository, sessionsRepository) { }

        public ErrorResponseModel ValidateProductModel(AddProductRequestModel addProductRequestModel)
        {
            if (addProductRequestModel == null)
                return new() { ErrorMessage = "no request model found" };

            if (addProductRequestModel.ProductName == null) 
                return new() { ErrorMessage = "no product name specified" };
            if (_productsRepository.GetProduct(addProductRequestModel.ProductName) is not null)
                return new() { ErrorMessage = "product with given name already exists" };

            if (addProductRequestModel.ProductId is not null && _productsRepository.GetProduct((int)addProductRequestModel.ProductId) is not null) 
                return new() { ErrorMessage = "product id already exists" };
            
            if (addProductRequestModel.ProductPrice < 0.01) return new() { ErrorMessage = "price can't be less than 0" };
            
            return null;
        }

        public Product ConvertToProduct(AddProductRequestModel product)
        {
            return new()
            {
                Name = product.ProductName,
                ProductId = product.ProductId,
                Quantity = product.ProductQuantity
            };
        }

        public AddProductSuccessModel AddProduct(Product product)
        {
            _productsRepository.CreateProduct(product);
            var addedProduct = _productsRepository.GetProduct(product.Name);
            return new()
            {
                ProductName = addedProduct.Name,
                ProductId = addedProduct.ProductId,
                ProductQuantity = addedProduct.Quantity,
                ProductPrice = addedProduct.Price,
                ProductCategory = addedProduct.Category
            };
        }

        public UpdateProductPriceSuccessModel UpdateProductPrice(UpdateProductPriceRequestModel product)
        {
            var updatedProduct = _productsRepository.GetProduct(product.ProductId);
            updatedProduct.Price = product.NewProductPrice;
            _productsRepository.UpdateProduct(updatedProduct);
            return new()
            {
                ProductName = updatedProduct.Name,
                ProductId = updatedProduct.ProductId,
                ProductQuantity = updatedProduct.Quantity,
                ProductPrice = updatedProduct.Price,
                ProductCategory = updatedProduct.Category
            };
        }

        public DeleteProductSuccessModel DeleteProduct(ActionWithExistingProductRequestModel product)
        {
            var deletedProduct = _productsRepository.GetProduct(product.ProductId);
            _productsRepository.DeleteProduct(deletedProduct);
            return new()
            {
                ProductName = deletedProduct.Name,
                ProductId = deletedProduct.ProductId,
                ProductQuantity = deletedProduct.Quantity,
                ProductPrice = deletedProduct.Price,
                ProductCategory = deletedProduct.Category
            };
        }
        
        public RejectOrderSuccessModel RejectOrder(RejectOrderRequestModel orderRequest)
        {
            var rejectedOrder = _ordersRepository.GetOrder(orderRequest.OrderId);
            if (rejectedOrder.Status == OrderStatus.Rejected) throw new ArgumentException("This order is already rejected");
            if (rejectedOrder.Status == OrderStatus.Sent) throw new ArgumentException("This order is already sent, can't reject it");
            rejectedOrder.Status = OrderStatus.Rejected;
            _ordersRepository.UpdateOrder(rejectedOrder);
            return new()
            {
                Order = rejectedOrder
            };
        }

        public string GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public string GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public string GetInternalInfo()
        {
            throw new NotImplementedException();
        }
    }
}
