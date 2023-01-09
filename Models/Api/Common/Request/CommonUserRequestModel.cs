using System.Text.Json.Serialization;

namespace Models.Api.Common.Request
{
    public abstract class CommonUserRequestModel
    {
        [JsonPropertyName("sessionId")]
        public int SessionId { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
    }
}