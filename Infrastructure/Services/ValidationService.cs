using System.Reflection;
using System.Text.RegularExpressions;
using Infrastructure.Interfaces;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Admin.Request;
using Models.Api.Req_Res.Common.Response;

namespace Infrastructure.Services
{
    [Obfuscation]
    public class ValidationService : IValidationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IProductsRepository _productsRepository;
        private const string EmailPattern = @"^\w[\w\+-]*(?:\.\w[\w\+-]*)*@[a-z]+(?:\.[a-z]{2,5})+$";
        private const string PhonePattern = @"^\+380\d{9}$";
        private const string NamePartPattern = "^[A-ZА-ЯЇҐЄІ][a-zа-яґїєі']*$";

        public ValidationService(IProductsRepository productsRepository, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _productsRepository = productsRepository;
        }

        public bool ValidateEmail(string? email, bool nullable = false)
        {
            if (email is null) return nullable;
            return Regex.IsMatch(email, EmailPattern);
        }

        public bool ValidatePhone(string? phone, bool nullable = false)
        {
            if (phone is null) return nullable;
            return Regex.IsMatch(phone, PhonePattern);
        }

        public bool ValidatePassword(string? password, bool nullable = false)
        {
            if (password is null) return nullable;
            return password.Length >= 8;
        }

        public bool ValidateName(string? name, bool nullable = false)
        {
            if (name is null) return nullable;
            string[] credentials = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string credential in credentials)
            {
                foreach (string namePart in credential.Split('-', StringSplitOptions.None))
                {
                    if (!Regex.IsMatch(namePart, NamePartPattern) || namePart[^1] == '\'') return false;
                }
            }
            
            return true;
        }

        public ErrorResponseModel? ValidateUserModel(UserModel user, bool nullable = false)
        {
            if (nullable && user.Login is not null && _usersRepository.GetUserByLogin(user.Login) is not null || !nullable && (user.Login is null || _usersRepository.GetUserByLogin(user.Login) is not null))
                return new() { ErrorMessage = "login is already in use" };
            if (!ValidateEmail(user.Email, nullable))
                return new() { ErrorMessage = "invalid email format" };
            if (!ValidateName(user.Name, nullable))
                return new() { ErrorMessage = "invalid name format" };
            if (!ValidatePhone(user.Phone, nullable))
                return new() { ErrorMessage = "invalid phone number format" };
            if (!ValidatePassword(user.Password, nullable))
                return new() { ErrorMessage = "invalid password format" };

            return null;
        }

        public ErrorResponseModel? ValidateProductModel(AddProductRequestModel? addProductRequestModel)
        {
            if (addProductRequestModel == null)
                return new() { ErrorMessage = "no request model found" };
            if (_productsRepository.GetProduct(addProductRequestModel.ProductName) is not null)
                return new() { ErrorMessage = "product with given name already exists" };
            if (addProductRequestModel.ProductPrice < 0.01) return new() { ErrorMessage = "price can't be less than 0" };
            
            return null;
        }
    }
}
