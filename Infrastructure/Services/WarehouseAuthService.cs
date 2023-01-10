using System.Reflection;
using Infrastructure.Interfaces;
using Models.Api.Req_Res.Common.Request;
using Models.Api.Req_Res.Common.Response;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Services;

[Obfuscation]
public class WarehouseAuthService : IWarehouseAuthService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ISessionsRepository _sessionsRepository;
    private readonly PasswordDecryptor _decryptor;

    public WarehouseAuthService(IUsersRepository usersRepository, ISessionsRepository sessionsRepository)
    {
        _usersRepository = usersRepository;
        _sessionsRepository = sessionsRepository;
        _decryptor = new PasswordDecryptor();
    }
    public TestLoginResponseModel TestLogin(TestLoginRequestModel login)
    {
        if (_usersRepository.GetUserByLogin(login.Login) is not null)
            return new TestLoginResponseModel()
            {
                Login = login.Login,
                Success = false,
                Message = "login is already in use"
            };
        return new TestLoginResponseModel()
        {
            Login = login.Login,
            Success = true,
            Message = "login is acceptable"
        };
    }

    public SignInResponseModel TrySignIn(SignInRequestModel userData)
    {
        if (_usersRepository.GetUserByLogin(userData.Login) is not null)
            return new SignInResponseModel()
            {
                Success = false,
                Message = "login is already in use"
            };
        string encryptedPassword = _decryptor.PrimaryEncryptPassword(userData.Login, userData.Password);
        _usersRepository.CreateUser(new User()
        {
            Login = userData.Login,
            Name = userData.Name,
            Email = userData.Email,
            EncryptedPassword = encryptedPassword,
            Phone = userData.Phone,
            Role = UserRole.Customer
        });
        User createdUser = _usersRepository.GetUserByLogin(userData.Login);
        createdUser.EncryptedPassword = _decryptor.SecondaryEncryptPassword(createdUser, createdUser.EncryptedPassword);
        _usersRepository.UpdateUser(createdUser);
        return new SignInResponseModel()
        {
            CreatedUser = createdUser,
            Success = true,
            Message = "user created successfully"
        };
    }

    public TryLogInResponseModel TryLogIn(LogInRequestModel credentials)
    {
        User user = _usersRepository.GetUserByLogin(credentials.Login);
        if (user is null)
            return new TryLogInResponseModel()
            {
                Success = false,
                Message = "login is not recognized"
            };
        if (!_decryptor.CheckPassword(user, credentials.Password))
        {
            return new TryLogInResponseModel()
            {
                Success = false,
                Message = "incorrect login or password"
            };
        }

        int sessionId = _sessionsRepository.CreateSessionAndGetSessionId(user);
        return new TryLogInResponseModel()
        {
            Success = true,
            Message = "successfully logged in",
            SessionId = sessionId
        };
    }
}