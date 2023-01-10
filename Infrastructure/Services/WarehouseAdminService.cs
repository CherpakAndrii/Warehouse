﻿using Infrastructure.Interfaces;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Admin.Request;
using Models.Api.Req_Res.Admin.Response;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services
{
    public class WarehouseAdminService : WarehouseUserService, IWarehouseAdminService
    {
        private readonly IUsersRepository _usersRepository;

        public WarehouseAdminService(IProductsRepository productsRepository, IUsersRepository usersRepository,
            IOrdersRepository ordersRepository, ISessionsRepository sessionsRepository) : base(productsRepository,
            ordersRepository, sessionsRepository)
        {
            _usersRepository = usersRepository;
        }

        public ErrorResponseModel ValidateProductModel(AddProductRequestModel addProductRequestModel)
        {
            if (addProductRequestModel == null)
                return new() { ErrorMessage = "no request model found" };

            if (addProductRequestModel.ProductName == null) 
                return new() { ErrorMessage = "no product name specified" };
            if (ProductsRepository.GetProduct(addProductRequestModel.ProductName) is not null)
                return new() { ErrorMessage = "product with given name already exists" };
            if (addProductRequestModel.ProductPrice < 0.01) return new() { ErrorMessage = "price can't be less than 0" };
            
            return null;
        }

        public AddProductSuccessModel AddProduct(AddProductRequestModel addProductRequest)
        {
            Product product = addProductRequest.ConvertToProduct();
            ProductsRepository.CreateProduct(product);
            var addedProduct = ProductsRepository.GetProduct(product.Name);
            return new() { Product = addedProduct };
        }

        public UpdateProductPriceSuccessModel UpdateProductPrice(UpdateProductPriceRequestModel product)
        {
            var updatedProduct = ProductsRepository.GetProduct(product.ProductId);
            updatedProduct.Price = product.NewProductPrice;
            ProductsRepository.UpdateProduct(updatedProduct);
            return new() { Product = updatedProduct };
        }

        public DeleteProductSuccessModel DeleteProduct(ActionWithExistingProductRequestModel product)
        {
            var deletedProduct = ProductsRepository.GetProduct(product.ProductId);
            ProductsRepository.DeleteProduct(deletedProduct);
            return new() { Product = deletedProduct };
        }
        
        public RejectOrderSuccessModel RejectOrder(RejectOrderRequestModel orderRequest)
        {
            var rejectedOrder = OrdersRepository.GetOrder(orderRequest.OrderId);
            if (rejectedOrder.Status == OrderStatus.Rejected) return new RejectOrderSuccessModel(){ Order = rejectedOrder, Success = false, Message = "This order is already rejected"};
            if (rejectedOrder.Status == OrderStatus.Sent) return new RejectOrderSuccessModel(){ Order = rejectedOrder, Success = false, Message = "This order is already sent, can't reject it"};
            var orderedProduct = rejectedOrder.Product;
            rejectedOrder.Status = OrderStatus.Rejected;
            orderedProduct.AvailableAmount += (int)rejectedOrder.Quantity;
            OrdersRepository.UpdateOrder(rejectedOrder);
            ProductsRepository.UpdateProduct(orderedProduct);
            return new()
            {
                Order = rejectedOrder,
                Success = true,
                Message = "Successfully rejected"
            };
        }

        public GetUserListSuccessModel GetUserList(GetUserListRequestModel getUserListRequest)
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
            if (_usersRepository.GetUserByLogin(addWorkerRequest.Login) is not null)
                return new AddWorkerResponseModel()
                {
                    Success = false,
                    Message = "login is already in use"
                };
            PasswordDecryptor decryptor = new PasswordDecryptor();
            string encryptedPassword = decryptor.EncryptPassword(addWorkerRequest.Login, addWorkerRequest.Password);
            _usersRepository.CreateUser(new User()
            {
                Login = addWorkerRequest.Login,
                Name = addWorkerRequest.Name,
                Email = addWorkerRequest.Email,
                EncryptedPassword = encryptedPassword,
                Phone = addWorkerRequest.Phone,
                Role = addWorkerRequest.Role
            });
            User createdUser = _usersRepository.GetUserByLogin(addWorkerRequest.Login);
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
