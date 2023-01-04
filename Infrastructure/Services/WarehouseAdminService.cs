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

            if (addProductRequestModel.ProductQuantity < 0) 
                return new() { ErrorMessage = "quantity cannot be less than 0" };

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

        public UpdateProductSuccessModel AddProductQuantity(int productID, uint quantityToAdd)
        {
            var addedProduct = _productsRepository.GetProduct(productID);
            addedProduct.Quantity += quantityToAdd;
            _productsRepository.UpdateProduct(addedProduct);
            return new()
            {
                ProductName = addedProduct.Name,
                ProductId = addedProduct.ProductId,
                ProductQuantity = addedProduct.Quantity
            };
        }

        // Sometimes something can happen to products (expiry, theft, fire damage). Therefore, the administrator should be able to reduce the number of products
        public UpdateProductSuccessModel DecreaseProductQuantity(int productID, uint quantityToSubtract)
        {
            var decreasedProduct = _productsRepository.GetProduct(productID);
            if (decreasedProduct.Quantity < quantityToSubtract) throw new ArgumentException("Impossible to remove more products than are available");
            decreasedProduct.Quantity -= quantityToSubtract;
            _productsRepository.UpdateProduct(decreasedProduct);
            return new()
            {
                ProductName = decreasedProduct.Name,
                ProductId = decreasedProduct.ProductId,
                ProductQuantity = decreasedProduct.Quantity
            };
        }

        public DeleteProductSuccessModel DeleteProduct(int productID)
        {
            var deletedProduct = _productsRepository.GetProduct(productID);
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
