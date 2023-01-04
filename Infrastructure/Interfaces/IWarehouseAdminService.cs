using Models.Api;
using Models.Api.Admin.Request;
using Models.Api.Admin.Response.Success;
using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseAdminService
    {
        ErrorResponseModel ValidateProductModel(AddProductRequestModel product);
        Product ConvertToProduct(AddProductRequestModel product);
        AddProductSuccessModel AddProduct(Product product);

        string AddProductQuantity(int productNumber);

        string GetAllCustomers();
        string GetAllOrders();
        string GetInternalInfo();
    }
}
