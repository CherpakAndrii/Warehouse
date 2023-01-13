using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Req_Res.Common.Request;

public class SignInRequestModel
{
    [JsonPropertyName("newUserLogin")]
    public string NewUserLogin { get; set; }
    [JsonPropertyName("newUserPassword")]
    public string NewUserPassword { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    public static implicit operator UserModel(SignInRequestModel request)
    {
        return new UserModel
        {
            UserId = -1,
            Login = request.NewUserLogin,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Password = request.NewUserPassword
        };
    }
}