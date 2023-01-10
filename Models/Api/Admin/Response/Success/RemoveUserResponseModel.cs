using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Admin.Response.Success;

public class RemoveUserResponseModel
{
    [JsonPropertyName("removedUser")]
    public UserModel RemovedUser { get; set; }
    
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; }
}