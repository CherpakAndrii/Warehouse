using Models.Api;
using Models.Api.Admin.Request;
using Models.Api.Admin.Response.Success;
using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseAdminService
    {
        ErrorResponseModel ValidateProductModel(AddProductRequestModel product);
        ErrorResponseModel TryFindProduct(DeleteProductRequestModel product);
        Product ConvertToProduct(AddProductRequestModel product);
        AddProductSuccessModel AddProduct(Product product);
        UpdateProductPriceSuccessModel UpdateProductPrice(UpdateProductPriceRequestModel product);
        DeleteProductSuccessModel DeleteProduct(DeleteProductRequestModel product);

        string GetAllCustomers();
        string GetAllOrders();
        string GetInternalInfo();
    }
}
