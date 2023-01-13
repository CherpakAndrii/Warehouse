using Models.Api.Req_Res.Admin.Request;
using Models.Api.Req_Res.Admin.Response;
using Models.Api.Req_Res.Common.Request;


namespace Infrastructure.Interfaces
{
    public interface IWarehouseAdminService
    {
        AddProductSuccessModel AddProduct(AddProductRequestModel product);
        UpdateProductPriceSuccessModel UpdateProductPrice(UpdateProductPriceRequestModel productRequest);
        DeleteProductSuccessModel DeleteProduct(ActionWithExistingProductRequestModel productRequest);
        RejectOrderSuccessModel RejectOrder(RejectOrderRequestModel orderRequest);


        GetUserListResponseModel GetUserList(GetUserListRequestModel getUserListRequest);
        AddWorkerResponseModel AddWorker(AddWorkerRequestModel addWorkerRequest);
        public RemoveUserResponseModel RemoveWorker(RemoveWorkerRequestModel removeWorkerRequest);
    }
}
