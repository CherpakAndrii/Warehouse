using System.Reflection;
using Infrastructure.Interfaces;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Admin.Request;
using Models.Api.Req_Res.Admin.Response;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services
{
    [Obfuscation]
    public class WarehouseAdminService : IWarehouseAdminService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersRepository _ordersRepository;

        public WarehouseAdminService(IProductsRepository productsRepository, IUsersRepository usersRepository,
            IOrdersRepository ordersRepository)
        {
            _usersRepository = usersRepository;
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            
        }

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

        public GetUserListResponseModel GetUserList(GetUserListRequestModel getUserListRequest)
        {
            var users = _usersRepository.GetAllUsers();
            List<UserModel> userModels = new List<UserModel>();
            foreach (var user in users)
            {
                userModels.Add(user);
            }
            return new()
            {
                UserList = userModels
            };
        }

        public AddWorkerResponseModel AddWorker(AddWorkerRequestModel addWorkerRequest)
        {
            if (_usersRepository.GetUserByLogin(addWorkerRequest.NewUserLogin) is not null)
                return new AddWorkerResponseModel()
                {
                    Success = false,
                    Message = "login is already in use"
                };
            PasswordDecryptor decryptor = new PasswordDecryptor();
            string encryptedPassword = decryptor.EncryptPassword(addWorkerRequest.NewUserLogin, addWorkerRequest.NewUserPassword);
            _usersRepository.CreateUser(new User()
            {
                Login = addWorkerRequest.NewUserLogin,
                Name = addWorkerRequest.Name,
                Email = addWorkerRequest.Email,
                EncryptedPassword = encryptedPassword,
                Phone = addWorkerRequest.Phone,
                Role = addWorkerRequest.Role
            });
            User createdUser = _usersRepository.GetUserByLogin(addWorkerRequest.NewUserLogin);
            return new AddWorkerResponseModel()
            {
                CreatedUser = createdUser,
                Success = true,
                Message = $"new {createdUser.Role.ToString()} created successfully"
            };
        }
        
        public RemoveUserResponseModel RemoveWorker(RemoveWorkerRequestModel removeWorkerRequest)
        {
            var deletedUser = _usersRepository.GetUser(removeWorkerRequest.UserId);
            if (deletedUser == null)
                return new() { Success = false, Message = "such user is not found" };
            _usersRepository.DeleteUser(deletedUser);
            return new()
            {
                Success = true, RemovedUser = deletedUser,
                Message = $"{deletedUser.Role.ToString()} deleted successfully"
            };
        }
    }
}
