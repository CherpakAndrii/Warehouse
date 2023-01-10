using System.Reflection;
using Infrastructure.Interfaces;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services
{
    [Obfuscation]
    public abstract class WarehouseUserService : IWarehouseUserService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly ISessionsRepository _sessionsRepository;
        private readonly IUsersRepository _usersRepository;

        public WarehouseUserService(IProductsRepository productsRepository, IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository , IUsersRepository usersRepository)
        {
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
            _sessionsRepository = sessionsRepository;
            _usersRepository = usersRepository;
        }

        public ErrorResponseModel TryFindProduct(ActionWithExistingProductRequestModel productRequest)
        {
            var product = _productsRepository.GetProduct(productRequest.ProductId);
            if (product is null) return new() { ErrorMessage = "product not found" };

            return null;
        }

        public ErrorResponseModel TryFindOrder(ActionWithExistingOrderRequestModel orderRequest)
        {
            var order = _ordersRepository.GetOrder(orderRequest.OrderId);
            if (order is null) return new() { ErrorMessage = "order not found" };

            return null;
        }

        public GetProductListSuccessModel GetProductsByCategory(GetProductListRequestModel productListRequest)
        {
            var products = _productsRepository.GetProductList(productListRequest.ProductCategory);
            List<ProductModel> productModels = new List<ProductModel>();
            foreach (var product in products)
            {
                productModels.Add(product);
            }

            return new()
            {
                Category = productListRequest.ProductCategory,
                ProductList = productModels
            };
        }
        
        public GetOrderListSuccessModel GetOrderList(GetOrderListRequestModel orderListRequest)
        {
            var orders = _ordersRepository.GetOrderList(orderListRequest.UserId, orderListRequest.ProductId);
            List<OrderModel> orderModels = new List<OrderModel>();
            foreach (var order in orders)
            {
                orderModels.Add(order);
            }

            return new()
            {
                OrderList = orderModels
            };
        }

        public (ErrorResponseModel, User) CheckRequest(CommonUserRequestModel request, AccessRights neededRights)
        {
            User requestUser = _sessionsRepository.GetUserBySessionId(request.SessionId);
            if (requestUser is null) return (new() { ErrorMessage = "401 Unauthorized" }, null);
            if (request.Login != requestUser.Login)
            {
                _sessionsRepository.CloseSessionById(request.SessionId); // someone is trying to use another user`s session!  
                return (new() { ErrorMessage = "409 Conflict" }, null);
            }
            var r = requestUser.Role;
            if (r == UserRole.Admin && (int)neededRights % 2 == 0 ||
                r == UserRole.Manager && (int)neededRights % 3 != 0 ||
                r == UserRole.Customer && (int)neededRights > 2)
                return (null, requestUser);
            return (new() { ErrorMessage = "403 Forbidden" }, null);
        }

        public (ErrorResponseModel, User) AdvancedCheckRequest(AdditionalSecurityRequestModel request, AccessRights neededRights)
        {
            var simpleCheckResult = CheckRequest(request, neededRights);
            if (simpleCheckResult.Item1 is not null) return simpleCheckResult;
            PasswordDecryptor pd = new PasswordDecryptor();
            if (!pd.CheckPassword(simpleCheckResult.Item2, request.CurrentPassword))
            {
                _sessionsRepository.CloseSessionById(request.SessionId); 
                return (new() { ErrorMessage = "409 Conflict" }, null);
            }

            return simpleCheckResult;
        }

        public GetMyProfileResponseModel GetMyProfileDetails(GetMyProfileRequestModel getMyProfileRequestModel)
        {
            var myProfile = _sessionsRepository.GetUserBySessionId(getMyProfileRequestModel.SessionId);
            return new()
            {
                Profile = myProfile
            };
        }

        public UpdateMyProfileResponseModel UpdateMyProfile(UpdateMyProfileRequestModel updateProfileRequest)
        {
            var myProfile = _sessionsRepository.GetUserBySessionId(updateProfileRequest.SessionId);
            int changesCtr = 0;
            if (updateProfileRequest.NewEmail is not null && updateProfileRequest.NewEmail != myProfile.Email)
            {
                changesCtr++;
                myProfile.Email = updateProfileRequest.NewEmail;
            }
            if (updateProfileRequest.NewName is not null && updateProfileRequest.NewName != myProfile.Name)
            {
                changesCtr++;
                myProfile.Name = updateProfileRequest.NewName;
            }
            if (updateProfileRequest.NewPhone is not null && updateProfileRequest.NewPhone != myProfile.Phone)
            {
                changesCtr++;
                myProfile.Phone = updateProfileRequest.NewPhone;
            }
            PasswordDecryptor pd = new();
            if (updateProfileRequest.NewLogin is not null && updateProfileRequest.NewLogin != myProfile.Login && _usersRepository.GetUserByLogin(updateProfileRequest.NewLogin) is null)
            {
                changesCtr++;
                myProfile.EncryptedPassword = pd.ReencryptPasswordOnLoginUpdate(myProfile.Login, updateProfileRequest.NewLogin, myProfile.EncryptedPassword);
                myProfile.Login = updateProfileRequest.NewLogin;
            }
            _usersRepository.UpdateUser(myProfile);
            
            if (updateProfileRequest.NewPassword is not null && pd.SecondaryEncryptPassword(myProfile, pd.PrimaryEncryptPassword(myProfile.Login, updateProfileRequest.NewPassword)) != myProfile.EncryptedPassword)
            {
                changesCtr++;
                myProfile.EncryptedPassword = pd.SecondaryEncryptPassword(myProfile,
                    pd.PrimaryEncryptPassword(myProfile.Login, updateProfileRequest.NewPassword));
                _usersRepository.UpdateUser(myProfile);
            }

            return new()
            {
                Profile = myProfile,
                ChangesCounter = changesCtr
            };
        }
    }
}
