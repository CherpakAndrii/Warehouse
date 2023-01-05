using Infrastructure.Interfaces;
using Models.Api;
using Models.Api.Admin.Request;
using Models.Api.Admin.Response.Success;
using Models.DBModels;

namespace Infrastructure.Services
{
    public class WarehouseAdminService : IWarehouseAdminService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IOrdersRepository _ordersRepository;

        public WarehouseAdminService(IProductsRepository productsRepository, ICustomersRepository customersRepository, IOrdersRepository ordersRepository)
        {
            _productsRepository = productsRepository;
            _customersRepository = customersRepository;
            _ordersRepository = ordersRepository;
        }

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

            if (addProductRequestModel.ProductQuantity < 0) 
                return new() { ErrorMessage = "quantity cannot be less than 0" };

            return null;
        }

        public ErrorResponseModel TryFindProduct(DeleteProductRequestModel productRequest)
        {
            var product = _productsRepository.GetProduct(productRequest.ProductId);
            if (product is null) return new() { ErrorMessage = "product not found" };

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
                ProductQuantity = addedProduct.Quantity
            };
        }

        public DeleteProductSuccessModel DeleteProduct(int productId)
        {
            var deletedProduct = _productsRepository.GetProduct(productId);
            _productsRepository.DeleteProduct(deletedProduct);
            return new()
            {
                ProductName = deletedProduct.Name,
                ProductId = deletedProduct.ProductId,
                ProductQuantity = deletedProduct.Quantity
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
