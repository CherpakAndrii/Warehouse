using Models.Api.Req_Res.Admin.Request;
using Models.Api.Req_Res.Admin.Response;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;


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
