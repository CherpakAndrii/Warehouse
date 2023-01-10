using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Request;

public class LogInRequestModel
{
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}