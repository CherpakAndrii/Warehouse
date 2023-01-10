using Models.Api.Admin.Request;
using Models.Api.Admin.Response;
using Models.Api.Admin.Response.Success;
using Models.Api.Common.Request;
using Models.Api.Common.Response;
using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseAdminService : IWarehouseUserService
    {
        ErrorResponseModel ValidateProductModel(AddProductRequestModel product);

        AddProductSuccessModel AddProduct(AddProductRequestModel product);
        UpdateProductPriceSuccessModel UpdateProductPrice(UpdateProductPriceRequestModel productRequest);
        DeleteProductSuccessModel DeleteProduct(ActionWithExistingProductRequestModel productRequest);
        RejectOrderSuccessModel RejectOrder(RejectOrderRequestModel orderRequest);


        GetUserListSuccessModel GetUserList(GetUserListRequestModel getUserListRequest);
        AddWorkerResponseModel AddWorker(AddWorkerRequestModel addWorkerRequest);
        public RemoveUserResponseModel RemoveWorker(RemoveWorkerRequestModel removeWorkerRequest);
    }
}
