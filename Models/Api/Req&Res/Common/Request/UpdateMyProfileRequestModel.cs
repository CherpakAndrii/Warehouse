#nullable enable
using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Req_Res.Common.Request;

public class UpdateMyProfileRequestModel : AdditionalSecurityRequestModel
{
    [JsonPropertyName("newLogin")]
    public string? NewLogin { get; set; }
    
    [JsonPropertyName("newName")]
    public string? NewName { get; set; }
    
    [JsonPropertyName("newEmail")]
    public string? NewEmail { get; set; }
    
    [JsonPropertyName("newPhone")]
    public string? NewPhone { get; set; }

    [JsonPropertyName("newPassword")]
    public string? NewPassword { get; set; }

    public static implicit operator UserModel(UpdateMyProfileRequestModel request)
    {
        return new UserModel
        {
            UserId = -1,
            Login = request.NewLogin,
            Email = request.NewEmail,
            Name = request.NewName,
            Phone = request.NewPhone,
            Password = request.NewPassword
        };
    }
}