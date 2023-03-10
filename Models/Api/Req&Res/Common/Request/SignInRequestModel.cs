using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Request;

public class SignInRequestModel
{
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}