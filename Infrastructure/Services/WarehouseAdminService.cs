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
            if (addProductRequestModel.ProductPrice < 0.01) return new() { ErrorMessage = "price can't be less than 0" };
            
            return null;
        }

        public AddProductSuccessModel AddProduct(AddProductRequestModel addProductRequest)
        {
            Product product = addProductRequest.ConvertToProduct();
            _productsRepository.CreateProduct(product);
            var addedProduct = _productsRepository.GetProduct(product.Name);
            return new() { Product = addedProduct };
        }

        public UpdateProductPriceSuccessModel UpdateProductPrice(UpdateProductPriceRequestModel product)
        {
            var updatedProduct = _productsRepository.GetProduct(product.ProductId);
            updatedProduct.Price = product.NewProductPrice;
            _productsRepository.UpdateProduct(updatedProduct);
            return new() { Product = updatedProduct };
        }

        public DeleteProductSuccessModel DeleteProduct(ActionWithExistingProductRequestModel product)
        {
            var deletedProduct = _productsRepository.GetProduct(product.ProductId);
            _productsRepository.DeleteProduct(deletedProduct);
            return new() { Product = deletedProduct };
        }
        
        public RejectOrderSuccessModel RejectOrder(RejectOrderRequestModel orderRequest)
        {
            var rejectedOrder = _ordersRepository.GetOrder(orderRequest.OrderId);
            if (rejectedOrder.Status == OrderStatus.Rejected) return new RejectOrderSuccessModel(){ Order = rejectedOrder, Success = false, Message = "This order is already rejected"};
            if (rejectedOrder.Status == OrderStatus.Sent) return new RejectOrderSuccessModel(){ Order = rejectedOrder, Success = false, Message = "This order is already sent, can't reject it"};
            var orderedProduct = rejectedOrder.Product;
            rejectedOrder.Status = OrderStatus.Rejected;
            orderedProduct.AvailableAmount += (int)rejectedOrder.Quantity;
            _ordersRepository.UpdateOrder(rejectedOrder);
            _productsRepository.UpdateProduct(orderedProduct);
            return new()
            {
                Order = rejectedOrder,
                Success = true,
                Message = "Successfully rejected"
            };
        }

        public string GetCustomersList()
        {
            throw new NotImplementedException();
        }

        public string AddWorker()
        {
            throw new NotImplementedException();
        }
    }
}
