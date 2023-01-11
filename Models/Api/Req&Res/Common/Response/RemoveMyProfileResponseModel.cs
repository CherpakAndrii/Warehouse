using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Response;

public class RemoveMyProfileResponseModel
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
}