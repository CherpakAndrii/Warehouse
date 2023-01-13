using System.Reflection;
using Infrastructure.Interfaces;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Admin.Request;
using Models.Api.Req_Res.Admin.Response;
using Models.Api.Req_Res.Common.Request;
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
        private readonly ISessionsRepository _sessionsRepository;

        public WarehouseAdminService(IProductsRepository productsRepository, IUsersRepository usersRepository,
            IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository)
        {
            _usersRepository = usersRepository;
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _sessionsRepository = sessionsRepository;
        }

        public AddProductSuccessModel AddProduct(AddProductRequestModel addProductRequest)
        {
            Product product = addProductRequest.ConvertToProduct();
            _productsRepository.CreateProduct(product);
            Product addedProduct = _productsRepository.GetProduct(product.Name)!;
            return new() { Product = addedProduct };
        }

        public UpdateProductPriceSuccessModel UpdateProductPrice(UpdateProductPriceRequestModel product)
        {
            Product updatedProduct = _productsRepository.GetProduct(product.ProductId)!;
            updatedProduct.Price = product.NewProductPrice;
            _productsRepository.UpdateProduct(updatedProduct);
            return new() { Product = updatedProduct };
        }

        public DeleteProductSuccessModel DeleteProduct(ActionWithExistingProductRequestModel product)
        {
            Product deletedProduct = _productsRepository.GetProduct(product.ProductId)!;
            _productsRepository.DeleteProduct(deletedProduct);
            return new() { Product = deletedProduct };
        }
        
        public RejectOrderSuccessModel RejectOrder(RejectOrderRequestModel orderRequest)
        {
            Order rejectedOrder = _ordersRepository.GetOrder(orderRequest.OrderId)!;
            if (rejectedOrder.Status == OrderStatus.Rejected) return new RejectOrderSuccessModel(){ Order = rejectedOrder, Success = false, Message = "This order is already rejected"};
            if (rejectedOrder.Status == OrderStatus.Sent) return new RejectOrderSuccessModel(){ Order = rejectedOrder, Success = false, Message = "This order is already sent, can't reject it"};
            Product orderedProduct = _productsRepository.GetProduct(rejectedOrder.ProductId)!;
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
            List<User> users = _usersRepository.GetAllUsers();
            List<UserModel> userModels = new List<UserModel>();
            foreach (User user in users)
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
            PasswordDecryptor decryptor = new PasswordDecryptor();
            string encryptedPassword = decryptor.PrimaryEncryptPassword(addWorkerRequest.NewUserLogin, addWorkerRequest.NewUserPassword);
            _usersRepository.CreateUser(new User()
            {
                Login = addWorkerRequest.NewUserLogin,
                Name = addWorkerRequest.Name,
                Email = addWorkerRequest.Email,
                EncryptedPassword = encryptedPassword,
                Phone = addWorkerRequest.Phone,
                Role = addWorkerRequest.Role
            });
            User createdUser = _usersRepository.GetUserByLogin(addWorkerRequest.NewUserLogin)!;
            createdUser.EncryptedPassword = decryptor.SecondaryEncryptPassword(createdUser, createdUser.EncryptedPassword);
            _usersRepository.UpdateUser(createdUser);
            return new AddWorkerResponseModel()
            {
                CreatedUser = createdUser,
                Success = true,
                Message = $"new {createdUser.Role.ToString()} created successfully"
            };
        }
        
        public RemoveUserResponseModel RemoveWorker(RemoveWorkerRequestModel removeWorkerRequest)
        {
            User? deletedUser = _usersRepository.GetUser(removeWorkerRequest.UserId);
            if (deletedUser == null)
                return new() { Success = false, Message = "such user is not found" };
            
            _sessionsRepository.CloseSessionForUser(deletedUser.UserId!.Value);
            _usersRepository.DeleteUser(deletedUser);
            return new()
            {
                Success = true, RemovedUser = deletedUser,
                Message = $"{deletedUser.Role.ToString()} deleted successfully"
            };
        }
    }
}
