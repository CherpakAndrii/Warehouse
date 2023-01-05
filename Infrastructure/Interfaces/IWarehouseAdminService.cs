using Models.Api.Admin.Request;
using Models.Api.Admin.Response.Success;
using Models.Api.Common.Request;
using Models.Api.Common.Response;
using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseAdminService : IWarehouseUserService
    {
        ErrorResponseModel ValidateProductModel(AddProductRequestModel product);

        Product ConvertToProduct(AddProductRequestModel product);
        AddProductSuccessModel AddProduct(Product product);
        UpdateProductPriceSuccessModel UpdateProductPrice(UpdateProductPriceRequestModel productRequest);
        DeleteProductSuccessModel DeleteProduct(ActionWithExistingProductRequestModel productRequest);
        RejectOrderSuccessModel RejectOrder(RejectOrderRequestModel orderRequest);


        string GetAllCustomers();
        string GetAllOrders();
        string GetInternalInfo();
    }
}
