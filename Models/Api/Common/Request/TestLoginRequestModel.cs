using System.Text.Json.Serialization;

namespace Models.Api.Common.Request;

public class TestLoginRequestModel
{
    [JsonPropertyName("login")]
    public string Login { get; set; }
}