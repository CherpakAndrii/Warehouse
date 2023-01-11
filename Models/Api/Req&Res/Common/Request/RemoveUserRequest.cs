using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Request;

public class RemoveUserRequest
{
    [JsonPropertyName("UserId")]
    public int UserId { get; set; }
}